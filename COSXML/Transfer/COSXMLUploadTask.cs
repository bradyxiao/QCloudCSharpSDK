﻿using System;
using System.Collections.Generic;
using System.Text;
using COSXML.Model;
using COSXML.CosException;
using COSXML.Model.Object;
using System.IO;
using COSXML.Common;
using COSXML.Utils;
using COSXML.Model.Tag;
using COSXML.Log;
/**
* Copyright (c) 2018 Tencent Cloud. All rights reserved.
* 11/29/2018 4:58:32 PM
* bradyxiao
*/
namespace COSXML.Transfer
{
    public sealed class COSXMLUploadTask : COSXMLTask, OnMultipartUploadStateListener
    {
        private int divisionSize;
        private int sliceSize;

        private long sendOffset = 0L;
        private long sendContentLength = -1L; // 实际要发送的总长度，类似于content-length
        private string srcPath;
        
        private PutObjectRequest putObjectRequest;

        private Object syncExit = new Object();
        private bool isExit = false;

        private ListPartsRequest listPartsRequest;

        private InitMultipartUploadRequest initMultiUploadRequest;
        private string uploadId;

        private Dictionary<UploadPartRequest, long> uploadPartRequestMap;
        private List<SliceStruct> sliceList;
        private Object syncPartCopyCount = new object();
        private int sliceCount;
        private long hasReceiveDataLength = 0;
        private object syncProgress = new Object();

        private CompleteMultiUploadRequest completeMultiUploadRequest;

        private AbortMultiUploadRequest abortMultiUploadRequest;

        public COSXMLUploadTask(string bucket, string region, string key)
            : base(bucket, region, key)
        {
        }

        internal void SetDivision(int divisionSize, int sliceSize)
        {
            this.divisionSize = divisionSize;
            this.sliceSize = sliceSize;
        }

        public void SetSrcPath(string srcPath, long fileOffset, long contentLength)
        {
            this.srcPath = srcPath;
            this.sendOffset = fileOffset >= 0 ? fileOffset : 0;
            this.sendContentLength = contentLength >= 0 ? contentLength : -1L;
        }

        public void SetUploadId(string uploadId)
        {
            this.uploadId = uploadId;
        }

        internal void Upload()
        {
            FileInfo fileInfo;
            try
            {
                fileInfo = new FileInfo(srcPath);
            }
            catch (Exception ex)
            {
                if (failCallback != null)
                {
                    failCallback(new CosClientException((int)CosClientError.INVALID_ARGUMENT, ex.Message, ex), null);
                }
                return;
            }

            long sourceLength = fileInfo.Length;
            if(sendContentLength == -1L || (sendContentLength + sendOffset > sourceLength))
            {
                sendContentLength = sourceLength - sendOffset;
            }
            if(sendContentLength > divisionSize)
            {
                MultiUpload();
            }
            else
            {
                SimpleUpload();
            }

        }

        private void SimpleUpload()
        {
            putObjectRequest = new PutObjectRequest(bucket, key, srcPath, sendOffset, sendContentLength);
            putObjectRequest.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
            if (progressCallback != null)
            {
                putObjectRequest.SetCosProgressCallback(progressCallback);
            }
            cosXmlServer.PutObject(putObjectRequest, delegate(CosResult cosResult)
            {
                PutObjectResult result = cosResult as PutObjectResult;
                UploadTaskResult copyTaskResult = new UploadTaskResult();
                copyTaskResult.SetResult(result);
                if (successCallback != null)
                {
                    successCallback(copyTaskResult);
                }

            },
                delegate(CosClientException clientEx, CosServerException serverEx)
                {
                    if (failCallback != null)
                    {
                        failCallback(clientEx, serverEx);
                    }
                });
        }

        private void MultiUpload()
        {
            ComputeSliceNums();
            if (uploadId != null)
            {
                ListMultiParts();
            }
            else
            {
                InitMultiUploadPart();
            }
        }

        private void InitMultiUploadPart()
        {
            initMultiUploadRequest = new InitMultipartUploadRequest(bucket, key);
            initMultiUploadRequest.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
            cosXmlServer.InitMultipartUpload(initMultiUploadRequest, delegate(CosResult cosResult)
            {
                lock (syncExit)
                {
                    if (isExit)
                    {
                        return;
                    }
                }
                InitMultipartUploadResult result = cosResult as InitMultipartUploadResult;
                uploadId = result.initMultipartUpload.uploadId;
                //通知执行PartCopy
                OnInit();

            },
            delegate(CosClientException clientEx, CosServerException serverEx)
            {
                lock (syncExit)
                {
                    if (isExit)
                    {
                        return;
                    }
                }
                OnFailed(clientEx, serverEx);

            });
        }

        private void ListMultiParts()
        {
            listPartsRequest = new ListPartsRequest(bucket, key, uploadId);
            listPartsRequest.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
            cosXmlServer.ListParts(listPartsRequest, delegate(CosResult cosResult)
            {
                lock (syncExit)
                {
                    if (isExit)
                    {
                        return;
                    }
                }
                ListPartsResult result = cosResult as ListPartsResult;
                //更细listParts
                UpdateSliceNums(result);
                //通知执行PartCopy
                OnInit();
               
            },
            delegate(CosClientException clientEx, CosServerException serverEx)
            {
                lock (syncExit)
                {
                    if (isExit)
                    {
                        return;
                    }
                }
                OnFailed(clientEx, serverEx);
            });
        }

        private void UploadPart()
        {
            int size = sliceList.Count;
            sliceCount = size;
            uploadPartRequestMap = new Dictionary<UploadPartRequest, long>(size);
            for (int i = 0; i < size; i++)
            {
                SliceStruct sliceStruct = sliceList[i];
                if (!sliceStruct.isAlreadyUpload)
                {
                    UploadPartRequest uploadPartRequest = new UploadPartRequest(bucket, key, sliceStruct.partNumber, uploadId, srcPath, sliceStruct.sliceStart,
                        sliceStruct.sliceLength);
                    uploadPartRequest.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
                    
                    //打印进度
                    uploadPartRequest.SetCosProgressCallback(delegate(long completed, long total)
                    {
                        lock (syncProgress)
                        {
                            long dataLen = hasReceiveDataLength + completed - uploadPartRequestMap[uploadPartRequest];
                            UpdateProgress(dataLen, sendContentLength, false);
                            hasReceiveDataLength = dataLen;
                            uploadPartRequestMap[uploadPartRequest] = completed;
                        }
                    });

                    uploadPartRequestMap.Add(uploadPartRequest, 0);

                    cosXmlServer.UploadPart(uploadPartRequest, delegate(CosResult result)
                    {
                        lock (syncExit)
                        {
                            if (isExit)
                            {
                                return;
                            }
                        }
                        UploadPartResult uploadPartResult = result as UploadPartResult;
                        sliceStruct.eTag = uploadPartResult.eTag;
                        lock (syncPartCopyCount)
                        {
                            sliceCount--;
                            if (sliceCount == 0)
                            {
                                OnPart();
                            }
                        }

                    }, delegate(CosClientException clientEx, CosServerException serverEx)
                    {
                        lock (syncExit)
                        {
                            if (isExit)
                            {
                                return;
                            }
                        }
                        OnFailed(clientEx, serverEx);
                    });

                }
            }
        }

        private void UpdateProgress(long complete, long total, bool isCompleted)
        {
            if (complete < total)
            {
                if (progressCallback != null)
                {
                    progressCallback(complete, total);
                }
            }
            else
            {
                if (isCompleted)
                {
                    if (progressCallback != null)
                    {
                        progressCallback(complete, total);
                    }
                }
                else
                {
                    if (progressCallback != null)
                    {
                        progressCallback(total - 1, total);
                    }
                }
            }
            
        }

        private void CompleteMultipartUpload()
        {
            completeMultiUploadRequest = new CompleteMultiUploadRequest(bucket, key, uploadId);
            foreach (SliceStruct sliceStruct in sliceList)
            {
                completeMultiUploadRequest.SetPartNumberAndETag(sliceStruct.partNumber, sliceStruct.eTag); // partNumberEtag 有序的
            }
            completeMultiUploadRequest.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
            cosXmlServer.CompleteMultiUpload(completeMultiUploadRequest, delegate(CosResult result)
            {
                lock (syncExit)
                {
                    if (isExit)
                    {
                        return;
                    }
                }
                CompleteMultiUploadResult completeMultiUploadResult = result as CompleteMultiUploadResult;
                OnCompleted(completeMultiUploadResult);

            }, delegate(CosClientException clientEx, CosServerException serverEx)
            {
                lock (syncExit)
                {
                    if (isExit)
                    {
                        return;
                    }
                }

                OnFailed(clientEx, serverEx);

            });
        } 

        private void ComputeSliceNums()
        {
            int count = (int)(sendContentLength / sliceSize);
            sliceList = new List<SliceStruct>(count > 0 ? count : 1);
            int i = 1;// partNumber >= 1
            for (; i < count; i++)
            {
                SliceStruct sliceStruct = new SliceStruct();
                sliceStruct.partNumber = i;
                sliceStruct.isAlreadyUpload = false;
                sliceStruct.sliceStart = sendOffset + (i - 1) * sliceSize;
                sliceStruct.sliceLength = sliceSize;
                sliceStruct.sliceEnd = sendOffset + i * sliceSize - 1;
                sliceList.Add(sliceStruct);
            }
            SliceStruct lastSliceStruct = new SliceStruct();
            lastSliceStruct.partNumber = i;
            lastSliceStruct.isAlreadyUpload = false;
            lastSliceStruct.sliceStart = sendOffset + (i - 1) * sliceSize;
            lastSliceStruct.sliceLength = sendContentLength  - (i - 1) * sliceSize;
            lastSliceStruct.sliceEnd = sendOffset + sendContentLength - 1;
            sliceList.Add(lastSliceStruct);
        }

        private void UpdateSliceNums(ListPartsResult listPartsResult)
        {
            try
            {
                if (listPartsResult.listParts.parts != null)
                {
                    //获取原来的parts并提取partNumber
                    Dictionary<int, SliceStruct> sourceParts = new Dictionary<int, SliceStruct>(sliceList.Count);
                    foreach (SliceStruct sliceStruct in sliceList)
                    {
                        sourceParts.Add(sliceStruct.partNumber, sliceStruct);
                    }
                    foreach (ListParts.Part part in listPartsResult.listParts.parts)
                    {
                        int partNumber = -1;
                        bool parse = int.TryParse(part.partNumber, out partNumber);
                        if (!parse) throw new ArgumentException("ListParts.Part parse error");
                        SliceStruct sliceStruct = sourceParts[partNumber];
                        sliceStruct.isAlreadyUpload = true;
                        sliceStruct.eTag = part.eTag;
                    }
                }
            }
            catch (Exception ex)
            {
                lock (syncExit)
                {
                    if (isExit)
                    {
                        return;
                    }
                }
                OnFailed(new CosClientException((int)CosClientError.INTERNA_LERROR, ex.Message, ex),  null);
            }

        }

        public void OnInit()
        {
            //获取了uploadId
            UploadPart();
        }

        public void OnPart()
        {
            //获取了 part ETag
            CompleteMultipartUpload();
        }

        public void OnCompleted(CompleteMultiUploadResult result)
        {
            UpdateProgress(sendContentLength, sendContentLength, true);
            lock (syncExit)
            {
                isExit = true;
            }
            if (successCallback != null)
            {
                UploadTaskResult uploadTaskResult = new UploadTaskResult();
                uploadTaskResult.SetResult(result);
                successCallback(uploadTaskResult);
            }
        }

        public void OnFailed(CosClientException clientEx, CosServerException serverEx)
        {
            lock (syncExit)
            {
                isExit = true;
            }
            if (failCallback != null)
            {
                failCallback(clientEx, serverEx);
            }
        }

        private void Abort()
        {
            abortMultiUploadRequest = new AbortMultiUploadRequest(bucket, key, uploadId);

        }

        private void Clear()
        {
            
        }

        public class UploadTaskResult : CosResult
        {
            public string eTag;

            public void SetResult(PutObjectResult result)
            {
                this.accessUrl = result.accessUrl;
                this.eTag = result.eTag;
                this.httpCode = result.httpCode;
                this.httpMessage = result.httpMessage;
                this.responseHeaders = result.responseHeaders;
            }

            public void SetResult(CompleteMultiUploadResult result)
            {
                this.accessUrl = result.accessUrl;
                this.eTag = result.completeMultipartUpload.eTag;
                this.httpCode = result.httpCode;
                this.httpMessage = result.httpMessage;
                this.responseHeaders = result.responseHeaders;
            }

            public override string GetResultInfo()
            {
                return base.GetResultInfo() + ("\n : ETag: " + eTag);
            }
        }
    }
}
