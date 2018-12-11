using System;
using System.Collections.Generic;

using System.Text;
using COSXML.Common;
using System.IO;
using COSXML.Log;
using COSXML.CosException;
using COSXML.Network;

namespace COSXML.Model.Object
{
    public sealed class UploadPartRequest : ObjectRequest
    {
        private static string TAG = typeof(UploadPartRequest).FullName;
        private int partNumber;
        private string uploadId;

        private string srcPath;
        private long fileOffset = -1L;
        private long contentLength = -1L;

        private byte[] data;

        private COSXML.Callback.OnProgressCallback progressCallback;


        private UploadPartRequest(string bucket, string key, int partNumber, string uploadId)
            : base(bucket, key)
        {
            this.method = CosRequestMethod.PUT;
            this.partNumber = partNumber;
            this.uploadId = uploadId;
        }

        public UploadPartRequest(string bucket, string key, int partNumber, string uploadId, string srcPath, long fileOffset,
            long fileSendLength)
            : this(bucket, key, partNumber, uploadId)
        {
            this.srcPath = srcPath;
            this.fileOffset = fileOffset < 0 ? 0 : fileOffset;
            this.contentLength = fileSendLength < 0 ? -1L : fileSendLength;
        }

        public UploadPartRequest(string bucket, string key, int partNumber, string uploadId, string srcPath)
            : this(bucket, key, partNumber, uploadId, srcPath, -1L, -1L)
        { }

        public UploadPartRequest(string bucket, string key, int partNumber, string uploadId, byte[] data)
            :this(bucket, key, partNumber, uploadId)
        {
            this.data = data;
        }

        public void SetPartNumber(int partNumber)
        {
            this.partNumber = partNumber;
        }

        public void SetUploadId(string uploadId)
        {
            this.uploadId = uploadId;
        }

        public void SetCosProgressCallback(COSXML.Callback.OnProgressCallback progressCallback)
        {
            this.progressCallback = progressCallback;
        }

        public override void CheckParameters()
        {
            base.CheckParameters();
            if (partNumber <= 0)
            {
                throw new CosClientException((int)CosClientError.INVALID_ARGUMENT, "partNumber < 1");
            }
            if (uploadId == null)
            {
                throw new CosClientException((int)CosClientError.INVALID_ARGUMENT, "uploadId = null");
            }
            if (srcPath == null && data == null) throw new CosClientException((int)(CosClientError.INVALID_ARGUMENT), "data source = null");
            if (srcPath != null)
            {
                if (!File.Exists(srcPath)) throw new CosClientException((int)(CosClientError.INVALID_ARGUMENT), "file not exist");
            }
        }

        protected override void InternalUpdateQueryParameters()
        {
            queryParameters.Add("uploadId", uploadId);
            queryParameters.Add("partNumber", partNumber.ToString());
        }

        public override Network.RequestBody GetRequestBody()
        {
            RequestBody body = null;
            if (srcPath != null)
            {
                FileInfo fileInfo = new FileInfo(srcPath);
                if (contentLength == -1 || contentLength + fileOffset > fileInfo.Length)
                {
                    contentLength = fileInfo.Length - fileOffset;
                }
                body = new FileRequestBody(srcPath, fileOffset, contentLength);
                body.ProgressCallback = progressCallback;
            }
            else if (data != null)
            {
                body = new ByteRequestBody(data);
                body.ProgressCallback = progressCallback;
            }
            return body;
        }
    }
}
