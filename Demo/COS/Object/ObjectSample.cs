using System;
using System.Collections.Generic;
using COSXML.Log;
using COSXML.Model.Object;
using COSXML.Utils;
using COSXML.Common;
using COSXML.Model;
using COSXML.CosException;
using System.Threading;
/**
* Copyright (c) 2018 Tencent Cloud. All rights reserved.
* 11/13/2018 4:35:27 PM
* bradyxiao
*/
namespace Demo.COS.Object
{
    public sealed class ObjectSample
    {

        public static void PutObject(COSXML.CosXml cosXml, string bucket, string key, string srcPath)
        { 
            try
            {
                PutObjectRequest request = new PutObjectRequest(bucket, key, srcPath);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

                //设置进度回调
                request.SetCosProgressCallback(delegate(long completed, long total)
                {
                    Console.WriteLine(String.Format("{0} progress = {1} / {2} : {3:##.##}%", DateTime.Now.ToString(), completed, total, completed * 100.0 / total));
                });

                //执行请求
                PutObjectResult result = cosXml.PutObject(request);

                Console.WriteLine(String.Format("{0}  {1}", DateTime.Now.ToString(), result.GetResultInfo()));
            }
            catch (CosClientException clientEx)
            {
                QLog.D("XIAO", clientEx.Message);
                Console.WriteLine("CosClientException: " + clientEx.StackTrace);
            }
            catch (CosServerException serverEx)
            {
                QLog.D("XIAO", serverEx.Message);
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }
        }

        public static void AsynPutObject(COSXML.CosXml cosXml, string bucket, string key, string srcPath)
        {
            QLog.D("XIAO", String.Format("currentThread id = {0}", Thread.CurrentThread.ManagedThreadId));
            PutObjectRequest request = new PutObjectRequest(bucket, key, srcPath);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

            //设置进度回调
            request.SetCosProgressCallback(delegate(long completed, long total)
            {
                Console.WriteLine(String.Format("progress = {0} / {1} : {2:##.##}%", completed, total, completed * 100.0 / total));
            });

            cosXml.PutObject(request, 
                delegate(CosResult cosResult)
                {
                    PutObjectResult result = cosResult as PutObjectResult;
                    Console.WriteLine(result.GetResultInfo());
                    Console.WriteLine(String.Format("currentThread id = {0}", Thread.CurrentThread.ManagedThreadId));
                },
                delegate(CosClientException clientEx, CosServerException serverEx)
                {
                    if (clientEx != null)
                    {
                        QLog.D("XIAO", clientEx.Message);
                        Console.WriteLine("CosClientException: " + clientEx.StackTrace);
                    }
                    if (serverEx != null)
                    {
                        QLog.D("XIAO", serverEx.Message);
                        Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                    }
                    Console.WriteLine(String.Format("currentThread id = {0}", Thread.CurrentThread.ManagedThreadId));
                });

        }

        public static void HeadObject(COSXML.CosXml cosXml, string bucket, string key)
        {
            try
            {
                HeadObjectRequest request = new HeadObjectRequest(bucket, key);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

                //执行请求
                HeadObjectResult result = cosXml.HeadObject(request);

                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                QLog.D("XIAO", clientEx.Message);
                Console.WriteLine("CosClientException: " + clientEx.StackTrace);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                QLog.D("XIAO", serverEx.Message);
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }
        }

        public static void AsynHeadObject(COSXML.CosXml cosXml, string bucket, string key)
        {
            QLog.D("XIAO", String.Format("currentThread id = {0}", Thread.CurrentThread.ManagedThreadId));
            HeadObjectRequest request = new HeadObjectRequest(bucket, key);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

            cosXml.HeadObject(request,
                delegate(CosResult cosResult)
                {
                    HeadObjectResult result = cosResult as HeadObjectResult;
                    Console.WriteLine(result.GetResultInfo());
                    Console.WriteLine(String.Format("currentThread id = {0}", Thread.CurrentThread.ManagedThreadId));
                },
                delegate(CosClientException clientEx, CosServerException serverEx)
                {
                    if (clientEx != null)
                    {
                        QLog.D("XIAO", clientEx.Message);
                        Console.WriteLine("CosClientException: " + clientEx.StackTrace);
                    }
                    if (serverEx != null)
                    {
                        QLog.D("XIAO", serverEx.Message);
                        Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                    }
                    Console.WriteLine(String.Format("currentThread id = {0}", Thread.CurrentThread.ManagedThreadId));
                });

        }

        public static void PutObjectACL(COSXML.CosXml cosXml, string bucket, string key)
        {
            try
            {
                PutObjectACLRequest request = new PutObjectACLRequest(bucket, key);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

                //添加acl
                request.SetCosACL(CosACL.PRIVATE);

                COSXML.Model.Tag.GrantAccount readAccount = new COSXML.Model.Tag.GrantAccount();
                readAccount.AddGrantAccount("1131975903", "1131975903");
                request.SetXCosGrantRead(readAccount);

                COSXML.Model.Tag.GrantAccount writeAccount = new COSXML.Model.Tag.GrantAccount();
                writeAccount.AddGrantAccount("1131975903", "1131975903");
                request.SetXCosGrantWrite(writeAccount);

                COSXML.Model.Tag.GrantAccount fullAccount = new COSXML.Model.Tag.GrantAccount();
                fullAccount.AddGrantAccount("2832742109", "2832742109");
                request.SetXCosReadWrite(fullAccount);

                //执行请求
                PutObjectACLResult result = cosXml.PutObjectACL(request);

                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                QLog.D("XIAO", clientEx.Message);
                Console.WriteLine("CosClientException: " + clientEx.StackTrace);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                QLog.D("XIAO", serverEx.Message);
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }
        }

        public static void AsynPutObjectACL(COSXML.CosXml cosXml, string bucket, string key)
        {
            QLog.D("XIAO", String.Format("currentThread id = {0}", Thread.CurrentThread.ManagedThreadId));
            PutObjectACLRequest request = new PutObjectACLRequest(bucket, key);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);


            //添加acl
            request.SetCosACL(CosACL.PRIVATE);

            COSXML.Model.Tag.GrantAccount readAccount = new COSXML.Model.Tag.GrantAccount();
            readAccount.AddGrantAccount("1131975903", "1131975903");
            request.SetXCosGrantRead(readAccount);

            COSXML.Model.Tag.GrantAccount writeAccount = new COSXML.Model.Tag.GrantAccount();
            writeAccount.AddGrantAccount("1131975903", "1131975903");
            request.SetXCosGrantWrite(writeAccount);

            COSXML.Model.Tag.GrantAccount fullAccount = new COSXML.Model.Tag.GrantAccount();
            fullAccount.AddGrantAccount("2832742109", "2832742109");
            request.SetXCosReadWrite(fullAccount);

            cosXml.PutObjectACL(request,
                delegate(CosResult cosResult)
                {
                    PutObjectACLResult result = cosResult as PutObjectACLResult;
                    Console.WriteLine(result.GetResultInfo());
                    Console.WriteLine(String.Format("currentThread id = {0}", Thread.CurrentThread.ManagedThreadId));
                },
                delegate(CosClientException clientEx, CosServerException serverEx)
                {
                    if (clientEx != null)
                    {
                        QLog.D("XIAO", clientEx.Message);
                        Console.WriteLine("CosClientException: " + clientEx.StackTrace);
                    }
                    if (serverEx != null)
                    {
                        QLog.D("XIAO", serverEx.Message);
                        Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                    }
                    Console.WriteLine(String.Format("currentThread id = {0}", Thread.CurrentThread.ManagedThreadId));
                });

        }

        public static void GetObjectACL(COSXML.CosXml cosXml, string bucket, string key)
        {
            try
            {
                GetObjectACLRequest request = new GetObjectACLRequest(bucket, key);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

                //执行请求
                GetObjectACLResult result = cosXml.GetObjectACL(request);

                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                QLog.D("XIAO", clientEx.Message);
                Console.WriteLine("CosClientException: " + clientEx.StackTrace);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                QLog.D("XIAO", serverEx.Message);
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }
        }

        public static void AsynGetObjectACL(COSXML.CosXml cosXml, string bucket, string key)
        {
            QLog.D("XIAO", String.Format("currentThread id = {0}", Thread.CurrentThread.ManagedThreadId));
            GetObjectACLRequest request = new GetObjectACLRequest(bucket, key);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

            cosXml.GetObjectACL(request,
                delegate(CosResult cosResult)
                {
                    GetObjectACLResult result = cosResult as GetObjectACLResult;
                    Console.WriteLine(result.GetResultInfo());
                    Console.WriteLine(String.Format("currentThread id = {0}", Thread.CurrentThread.ManagedThreadId));
                },
                delegate(CosClientException clientEx, CosServerException serverEx)
                {
                    if (clientEx != null)
                    {
                        QLog.D("XIAO", clientEx.Message);
                        Console.WriteLine("CosClientException: " + clientEx.StackTrace);
                    }
                    if (serverEx != null)
                    {
                        QLog.D("XIAO", serverEx.Message);
                        Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                    }
                    Console.WriteLine(String.Format("currentThread id = {0}", Thread.CurrentThread.ManagedThreadId));
                });

        }


        public static void OptionObject(COSXML.CosXml cosXml, string bucket, string key, string origin, string accessMthod)
        {
            try
            {
                OptionObjectRequest request = new OptionObjectRequest(bucket, key, origin, accessMthod);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

                //执行请求
                OptionObjectResult result = cosXml.OptionObject(request);

                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                QLog.D("XIAO", clientEx.Message);
                Console.WriteLine("CosClientException: " + clientEx.StackTrace);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                QLog.D("XIAO", serverEx.Message);
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }
        }

        public static void AsynOptionObject(COSXML.CosXml cosXml, string bucket, string key, string origin, string accessMthod)
        {
            QLog.D("XIAO", String.Format("currentThread id = {0}", Thread.CurrentThread.ManagedThreadId));
            OptionObjectRequest request = new OptionObjectRequest(bucket, key, origin, accessMthod);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

            cosXml.OptionObject(request,
                delegate(CosResult cosResult)
                {
                    OptionObjectResult result = cosResult as OptionObjectResult;
                    Console.WriteLine(result.GetResultInfo());
                    Console.WriteLine(String.Format("currentThread id = {0}", Thread.CurrentThread.ManagedThreadId));
                },
                delegate(CosClientException clientEx, CosServerException serverEx)
                {
                    if (clientEx != null)
                    {
                        QLog.D("XIAO", clientEx.Message);
                        Console.WriteLine("CosClientException: " + clientEx.StackTrace);
                    }
                    if (serverEx != null)
                    {
                        QLog.D("XIAO", serverEx.Message);
                        Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                    }
                    Console.WriteLine(String.Format("currentThread id = {0}", Thread.CurrentThread.ManagedThreadId));
                });

        }

        public static void CopyObject(COSXML.CosXml cosXml, string bucket, string key, COSXML.Model.Tag.CopySourceStruct copySource)
        {
            try
            {
                CopyObjectRequest request = new CopyObjectRequest(bucket, key);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

                //设置拷贝源
                request.SetCopySource(copySource);

                //设置是否拷贝还是更新
                request.SetCopyMetaDataDirective(COSXML.Common.CosMetaDataDirective.COPY);

                //执行请求
                CopyObjectResult result = cosXml.CopyObject(request);

                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                QLog.D("XIAO", clientEx.Message);
                Console.WriteLine("CosClientException: " + clientEx.StackTrace);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                QLog.D("XIAO", serverEx.Message);
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }
        }

        public static void AsynCopyObject(COSXML.CosXml cosXml, string bucket, string key, COSXML.Model.Tag.CopySourceStruct copySource)
        {
            QLog.D("XIAO", String.Format("currentThread id = {0}", Thread.CurrentThread.ManagedThreadId));
            CopyObjectRequest request =  new CopyObjectRequest(bucket, key);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

            //设置拷贝源
            request.SetCopySource(copySource);

            //设置是否拷贝还是更新
            request.SetCopyMetaDataDirective(COSXML.Common.CosMetaDataDirective.COPY);

            cosXml.CopyObject(request,
                delegate(CosResult cosResult)
                {
                    CopyObjectResult result = cosResult as CopyObjectResult;
                    Console.WriteLine(result.GetResultInfo());
                    Console.WriteLine(String.Format("currentThread id = {0}", Thread.CurrentThread.ManagedThreadId));
                },
                delegate(CosClientException clientEx, CosServerException serverEx)
                {
                    if (clientEx != null)
                    {
                        QLog.D("XIAO", clientEx.Message);
                        Console.WriteLine("CosClientException: " + clientEx.StackTrace);
                    }
                    if (serverEx != null)
                    {
                        QLog.D("XIAO", serverEx.Message);
                        Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                    }
                    Console.WriteLine(String.Format("currentThread id = {0}", Thread.CurrentThread.ManagedThreadId));
                });

        }

        public static void InitMultiUpload(COSXML.CosXml cosXml, string bucket, string key)
        {
            try
            {
                InitMultipartUploadRequest request = new InitMultipartUploadRequest(bucket, key);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

                //执行请求
                InitMultipartUploadResult result = cosXml.InitMultipartUpload(request);

                Console.WriteLine(result.GetResultInfo());
                QLog.D("XIAO", result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                QLog.D("XIAO", clientEx.Message);
                Console.WriteLine("CosClientException: " + clientEx.StackTrace);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                QLog.D("XIAO", serverEx.Message);
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }
        }

        public static void AsynInitMultiUpload(COSXML.CosXml cosXml, string bucket, string key)
        {
            QLog.D("XIAO", String.Format("currentThread id = {0}", Thread.CurrentThread.ManagedThreadId));
            InitMultipartUploadRequest request = new InitMultipartUploadRequest(bucket, key);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

            cosXml.InitMultipartUpload(request,
                delegate(CosResult cosResult)
                {
                    InitMultipartUploadResult result = cosResult as InitMultipartUploadResult;
                    Console.WriteLine(result.GetResultInfo());
                    Console.WriteLine(String.Format("currentThread id = {0}", Thread.CurrentThread.ManagedThreadId));
                    QLog.D("XIAO", result.GetResultInfo());
                },
                delegate(CosClientException clientEx, CosServerException serverEx)
                {
                    if (clientEx != null)
                    {
                        QLog.D("XIAO", clientEx.Message);
                        Console.WriteLine("CosClientException: " + clientEx.StackTrace);
                    }
                    if (serverEx != null)
                    {
                        QLog.D("XIAO", serverEx.Message);
                        Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                    }
                    Console.WriteLine(String.Format("currentThread id = {0}", Thread.CurrentThread.ManagedThreadId));
                });

        }

        public static void ListParts(COSXML.CosXml cosXml, string bucket, string key, string uploadId)
        {
            try
            {
                ListPartsRequest request = new ListPartsRequest(bucket, key, uploadId);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

                //执行请求
                ListPartsResult result = cosXml.ListParts(request);

                QLog.D("XIAO", result.GetResultInfo());
                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                QLog.D("XIAO", clientEx.Message);
                Console.WriteLine("CosClientException: " + clientEx.Message);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                QLog.D("XIAO", serverEx.Message);
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }
        }

        public static void AsynListParts(COSXML.CosXml cosXml, string bucket, string key, string uploadId)
        {
            QLog.D("XIAO", String.Format("currentThread id = {0}", Thread.CurrentThread.ManagedThreadId));
            ListPartsRequest request = new ListPartsRequest(bucket, key, uploadId);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

            cosXml.ListParts(request,
                delegate(CosResult cosResult)
                {
                    ListPartsResult result = cosResult as ListPartsResult;
                    Console.WriteLine(result.GetResultInfo());
                    Console.WriteLine(String.Format("currentThread id = {0}", Thread.CurrentThread.ManagedThreadId));
                    QLog.D("XIAO", result.GetResultInfo());
                },
                delegate(CosClientException clientEx, CosServerException serverEx)
                {
                    if (clientEx != null)
                    {
                        QLog.D("XIAO", clientEx.Message);
                        Console.WriteLine("CosClientException: " + clientEx.StackTrace);
                    }
                    if (serverEx != null)
                    {
                        QLog.D("XIAO", serverEx.Message);
                        Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                    }
                    Console.WriteLine(String.Format("currentThread id = {0}", Thread.CurrentThread.ManagedThreadId));
                });

        }

        public static void UploadParts(COSXML.CosXml cosXml, string bucket, string key, string uploadId, int partNumber, string srcPath)
        {
            try
            {
                UploadPartRequest request = new UploadPartRequest(bucket, key, partNumber, uploadId, srcPath);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

                //设置进度回调
                request.SetCosProgressCallback(delegate(long completed, long total)
                {
                    Console.WriteLine(String.Format("{0} progress = {1} / {2} : {3:##.##}%", DateTime.Now.ToString(), completed, total, completed * 100.0 / total));
                });

                //执行请求
                UploadPartResult result = cosXml.UploadPart(request);

                Console.WriteLine(result.GetResultInfo());
                QLog.D("XIAO", result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                QLog.D("XIAO", clientEx.Message);
                Console.WriteLine("CosClientException: " + clientEx.StackTrace);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                QLog.D("XIAO", serverEx.Message);
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }
        }


        public static void AsynUploadParts(COSXML.CosXml cosXml, string bucket, string key, string uploadId, int partNumber, string srcPath)
        {
            UploadPartRequest request = new UploadPartRequest(bucket, key, partNumber, uploadId, srcPath);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

            request.SetCosProgressCallback(delegate(long completed, long total)
            {
                Console.WriteLine(String.Format("progress = {0} / {1} : {2:##.##}%", completed, total, completed * 100.0 / total));
            });

            //执行请求
            cosXml.UploadPart(request, delegate(CosResult result)
            {
                UploadPartResult getObjectResult = result as UploadPartResult;
                Console.WriteLine(getObjectResult.GetResultInfo());
                QLog.D("XIAO", result.GetResultInfo());
            }, delegate(CosClientException clientEx, CosServerException serverEx)
            {
                if (clientEx != null)
                {
                    QLog.D("XIAO", clientEx.Message);
                    Console.WriteLine("CosClientException: " + clientEx.StackTrace);
                }
                if (serverEx != null)
                {
                    QLog.D("XIAO", serverEx.Message);
                    Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                }
            });
        }

        public static void CompleteMultiUpload(COSXML.CosXml cosXml, string bucket, string key, string uploadId, Dictionary<int, string> partNumberAndEtags)
        {
            try
            {
                CompleteMultipartUploadRequest request = new CompleteMultipartUploadRequest(bucket, key, uploadId);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

                //设置已上传的parts
                request.SetPartNumberAndETag(partNumberAndEtags);

                //执行请求
                CompleteMultipartUploadResult result = cosXml.CompleteMultiUpload(request);

                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                QLog.D("XIAO", clientEx.Message);
                Console.WriteLine("CosClientException: " + clientEx.StackTrace);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                QLog.D("XIAO", serverEx.Message);
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }
        }

        public static void AsynCompleteMultiUpload(COSXML.CosXml cosXml, string bucket, string key, string uploadId, Dictionary<int, string> partNumberAndEtags)
        {
            CompleteMultipartUploadRequest request = new CompleteMultipartUploadRequest(bucket, key, uploadId);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

            //设置已上传的parts
            request.SetPartNumberAndETag(partNumberAndEtags);

            //执行请求
            cosXml.CompleteMultiUpload(request, delegate(CosResult result)
            {
                CompleteMultipartUploadResult getObjectResult = result as CompleteMultipartUploadResult;
                Console.WriteLine(getObjectResult.GetResultInfo());
                QLog.D("XIAO", result.GetResultInfo());
            }, delegate(CosClientException clientEx, CosServerException serverEx)
            {
                if (clientEx != null)
                {
                    QLog.D("XIAO", clientEx.Message);
                    Console.WriteLine("CosClientException: " + clientEx.StackTrace);
                }
                if (serverEx != null)
                {
                    QLog.D("XIAO", serverEx.Message);
                    Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                }
            });
        }

        public static void AbortMultiUpload(COSXML.CosXml cosXml, string bucket, string key, string uploadId)
        {
            try
            {
                AbortMultipartUploadRequest request = new AbortMultipartUploadRequest(bucket, key, uploadId);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
                //执行请求
                AbortMultipartUploadResult result = cosXml.AbortMultiUpload(request);

                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                QLog.D("XIAO", clientEx.Message);
                Console.WriteLine("CosClientException: " + clientEx.StackTrace);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                QLog.D("XIAO", serverEx.Message);
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }
        }

        public static void AsynAbortMultiUpload(COSXML.CosXml cosXml, string bucket, string key, string uploadId)
        {
            AbortMultipartUploadRequest request = new AbortMultipartUploadRequest(bucket, key, uploadId);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

            //执行请求
            cosXml.AbortMultiUpload(request, delegate(CosResult result)
            {
                AbortMultipartUploadResult getObjectResult = result as AbortMultipartUploadResult;
                Console.WriteLine(getObjectResult.GetResultInfo());
            }, delegate(CosClientException clientEx, CosServerException serverEx)
            {
                if (clientEx != null)
                {
                    QLog.D("XIAO", clientEx.Message);
                    Console.WriteLine("CosClientException: " + clientEx.StackTrace);
                }
                if (serverEx != null)
                {
                    QLog.D("XIAO", serverEx.Message);
                    Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                }
            });
        }

        public static void PartCopyObject(COSXML.CosXml cosXml, string bucket, string key, 
            COSXML.Model.Tag.CopySourceStruct copySource, string uploadId, int partNumber)
        {
            try
            {
                UploadPartCopyRequest request = new UploadPartCopyRequest(bucket, key, partNumber, uploadId);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

                //设置拷贝源
                request.SetCopySource(copySource);

                //设置拷贝范围
                request.SetCopyRange(0, 10);

                //执行请求
                UploadPartCopyResult result = cosXml.PartCopy(request);

                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                QLog.D("XIAO", clientEx.Message);
                Console.WriteLine("CosClientException: " + clientEx.StackTrace);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                QLog.D("XIAO", serverEx.Message);
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }
        }

        public static void AsynPartCopyObject(COSXML.CosXml cosXml, string bucket, string key,
            COSXML.Model.Tag.CopySourceStruct copySource, string uploadId, int partNumber)
        {
            UploadPartCopyRequest request = new UploadPartCopyRequest(bucket, key, partNumber, uploadId);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

            //设置拷贝源
            request.SetCopySource(copySource);

            //设置拷贝范围
            request.SetCopyRange(0, 10);

            //执行请求
            cosXml.PartCopy(request, delegate(CosResult result)
            {
                UploadPartCopyResult getObjectResult = result as UploadPartCopyResult;
                Console.WriteLine(getObjectResult.GetResultInfo());
            }, delegate(CosClientException clientEx, CosServerException serverEx)
            {
                if (clientEx != null)
                {
                    QLog.D("XIAO", clientEx.Message);
                    Console.WriteLine("CosClientException: " + clientEx.StackTrace);
                }
                if (serverEx != null)
                {
                    QLog.D("XIAO", serverEx.Message);
                    Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                }
            });
        }

        public static void RestoreObject(COSXML.CosXml cosXml, string bucket, string key)
        {
            try
            {
                RestoreObjectRequest request = new RestoreObjectRequest(bucket, key);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

                //恢复时间
                request.SetExpireDays(3);
                request.SetTier(COSXML.Model.Tag.RestoreConfigure.Tier.Bulk);

                //执行请求
                RestoreObjectResult result = cosXml.RestoreObject(request);

                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                QLog.D("XIAO", clientEx.Message);
                Console.WriteLine("CosClientException: " + clientEx.StackTrace);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                QLog.D("XIAO", serverEx.Message);
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }
        }

        public static void AsynRestoreObject(COSXML.CosXml cosXml, string bucket, string key)
        {
            RestoreObjectRequest request = new RestoreObjectRequest(bucket, key);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

            //恢复时间
            request.SetExpireDays(3);
            request.SetTier(COSXML.Model.Tag.RestoreConfigure.Tier.Bulk);

            //执行请求
            cosXml.RestoreObject(request, delegate(CosResult result)
            {
                RestoreObjectResult getObjectResult = result as RestoreObjectResult;
                Console.WriteLine(getObjectResult.GetResultInfo());
            }, delegate(CosClientException clientEx, CosServerException serverEx)
            {
                if (clientEx != null)
                {
                    QLog.D("XIAO", clientEx.Message);
                    Console.WriteLine("CosClientException: " + clientEx.StackTrace);
                }
                if (serverEx != null)
                {
                    QLog.D("XIAO", serverEx.Message);
                    Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                }
            });
        }

        public static void PostObject(COSXML.CosXml cosXml, string bucket, string key, string srcPath, PostObjectRequest.Policy policy)
        {
            try
            {
                PostObjectRequest request = new PostObjectRequest(bucket, key, srcPath);
                //设置签名有效时长
                //request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
                List<string> headers = new List<string>();
                headers.Add("Host");
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600, headers, null);

                request.SetCosProgressCallback(delegate(long completed, long total)
                {
                    Console.WriteLine(String.Format("progress = {0} / {1} : {2:##.##}%", completed, total, completed * 100.0 / total));
                });

                //设置policy
                request.SetPolicy(policy);

                //执行请求
                PostObjectResult result = cosXml.PostObject(request);

                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                QLog.D("XIAO", clientEx.Message);
                Console.WriteLine("CosClientException: " + clientEx.StackTrace);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                QLog.D("XIAO", serverEx.Message);
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }
        }


        public static void AsynPostObject(COSXML.CosXml cosXml, string bucket, string key, string srcPath, PostObjectRequest.Policy policy)
        {
            PostObjectRequest request = new PostObjectRequest(bucket, key, srcPath);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

            request.SetCosProgressCallback(delegate(long completed, long total)
            {
                Console.WriteLine(String.Format("progress = {0} / {1} : {2:##.##}%", completed, total, completed * 100.0 / total));
            });

            //设置policy
            request.SetPolicy(policy);

            //执行请求
            cosXml.PostObject(request, delegate(CosResult result)
            {
                PostObjectResult getObjectResult = result as PostObjectResult;
                Console.WriteLine(getObjectResult.GetResultInfo());
            }, delegate(CosClientException clientEx, CosServerException serverEx)
            {
                if (clientEx != null)
                {
                    QLog.D("XIAO", clientEx.Message);
                    Console.WriteLine("CosClientException: " + clientEx.StackTrace);
                }
                if (serverEx != null)
                {
                    QLog.D("XIAO", serverEx.Message);
                    Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                }
            });
        }


        public static void GetObject(COSXML.CosXml cosXml, string bucket, string key, string localDir, string localFileName)
        {

            try
            {
                GetObjectRequest request = new GetObjectRequest(bucket, key, localDir, localFileName);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

                request.SetCosProgressCallback(delegate(long completed, long total)
                {
                    Console.WriteLine(String.Format("progress = {0} / {1} : {2:##.##}%", completed, total, completed * 100.0 / total));
                });

                //执行请求
                GetObjectResult result = cosXml.GetObject(request);

                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                QLog.D("XIAO", clientEx.Message);
                Console.WriteLine("CosClientException: " + clientEx.StackTrace);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                QLog.D("XIAO", serverEx.Message);
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }
        }


        public static void AsyncGetObject(COSXML.CosXml cosXml, string bucket, string key, string localDir, string localFileName)
        {
            GetObjectRequest request = new GetObjectRequest(bucket, key, localDir, localFileName);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

            request.SetCosProgressCallback(delegate(long completed, long total)
            {
                Console.WriteLine(String.Format("progress = {0} / {1} : {2:##.##}%", completed, total, completed * 100.0 / total));
            });

            //执行请求
            cosXml.GetObject(request, delegate(CosResult result) 
            {
                GetObjectResult getObjectResult = result as GetObjectResult;
                Console.WriteLine(getObjectResult.GetResultInfo());
            }, delegate(CosClientException clientEx, CosServerException serverEx) 
            {
                if (clientEx != null)
                {
                    QLog.D("XIAO", clientEx.Message);
                    Console.WriteLine("CosClientException: " + clientEx.StackTrace);
                }
                if (serverEx != null)
                {
                    QLog.D("XIAO", serverEx.Message);
                    Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                }
            });
        }

        public static void DeleteObject(COSXML.CosXml cosXml, string bucket, string key)
        {
            try
            {
                DeleteObjectRequest request = new DeleteObjectRequest(bucket, key);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

                //执行请求
                DeleteObjectResult result = cosXml.DeleteObject(request);

                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                QLog.D("XIAO", clientEx.Message);
                Console.WriteLine("CosClientException: " + clientEx.StackTrace);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                QLog.D("XIAO", serverEx.Message);
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }
        }


        public static void AsynDeleteObject(COSXML.CosXml cosXml, string bucket, string key)
        {
            DeleteObjectRequest request = new DeleteObjectRequest(bucket, key);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

            //执行请求
            cosXml.DeleteObject(request, delegate(CosResult result)
            {
                DeleteObjectResult getObjectResult = result as DeleteObjectResult;
                Console.WriteLine(getObjectResult.GetResultInfo());
            }, delegate(CosClientException clientEx, CosServerException serverEx)
            {
                if (clientEx != null)
                {
                    QLog.D("XIAO", clientEx.Message);
                    Console.WriteLine("CosClientException: " + clientEx.StackTrace);
                }
                if (serverEx != null)
                {
                    QLog.D("XIAO", serverEx.Message);
                    Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                }
            });
        }

        public static void MultiDeleteObject(COSXML.CosXml cosXml, string bucket, List<string> keys)
        {
            try
            {
                DeleteMultiObjectRequest request = new DeleteMultiObjectRequest(bucket);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

                //设置返回结果形式
                request.SetDeleteQuiet(false);

                //设置删除的keys
                request.SetObjectKeys(keys);

                //执行请求
                DeleteMultiObjectResult result = cosXml.DeleteMultiObjects(request);

                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                QLog.D("XIAO", clientEx.Message);
                Console.WriteLine("CosClientException: " + clientEx.StackTrace);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                QLog.D("XIAO", serverEx.Message);
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }
        }

        public static void AsyncMultiDeleteObject(COSXML.CosXml cosXml, string bucket, List<string> keys)
        {
            DeleteMultiObjectRequest request = new DeleteMultiObjectRequest(bucket);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

            //设置返回结果形式
            request.SetDeleteQuiet(false);

            //设置删除的keys
            request.SetObjectKeys(keys);

            //执行请求
            cosXml.DeleteMultiObjects(request, delegate(CosResult result)
            {
                DeleteMultiObjectResult getObjectResult = result as DeleteMultiObjectResult;
                Console.WriteLine(getObjectResult.GetResultInfo());
            }, delegate(CosClientException clientEx, CosServerException serverEx)
            {
                if (clientEx != null)
                {
                    QLog.D("XIAO", clientEx.Message);
                    Console.WriteLine("CosClientException: " + clientEx.StackTrace);
                }
                if (serverEx != null)
                {
                    QLog.D("XIAO", serverEx.Message);
                    Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                }
            });
        }
    }
}
