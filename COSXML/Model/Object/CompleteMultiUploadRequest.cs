using System;
using System.Collections.Generic;

using System.Text;
using COSXML.Model.Tag;
using COSXML.Common;
using COSXML.CosException;
using COSXML.Network;

namespace COSXML.Model.Object
{
    public sealed class CompleteMultiUploadRequest : ObjectRequest
    {
        private CompleteMultipartUpload completeMultipartUpload;
        private string uploadId;

        public CompleteMultiUploadRequest(string bucket, string key, string uploadId)
            : base(bucket, key)
        {
            this.method = CosRequestMethod.POST;
            this.needMD5 = true;
            this.uploadId = uploadId;
            completeMultipartUpload = new CompleteMultipartUpload();
            completeMultipartUpload.parts = new List<CompleteMultipartUpload.Part>();
        }

        public void SetUploadId(string uploadId)
        {
            this.uploadId = uploadId;
        }

        public void SetPartNumberAndETag(int partNumber, string eTag)
        {
            CompleteMultipartUpload.Part part = new CompleteMultipartUpload.Part();
            part.partNumber = partNumber;
            part.eTag = eTag;
            completeMultipartUpload.parts.Add(part);
        }

        public void SetPartNumberAndETag(Dictionary<int, string> partNumberAndETags)
        {
            if (partNumberAndETags != null)
            {
                foreach (KeyValuePair<int, string> pair in partNumberAndETags)
                {
                    SetPartNumberAndETag(pair.Key, pair.Value);
                }
            }
        }

        public override Network.RequestBody GetRequestBody()
        {
            string content = Transfer.XmlBuilder.BuildCompleteMultipartUpload(completeMultipartUpload);
            byte[] data = Encoding.UTF8.GetBytes(content);
            ByteRequestBody body = new ByteRequestBody(data);
            return body;
        }

        public override void CheckParameters()
        {
            base.CheckParameters();
            if (uploadId == null)
            {
                throw new CosClientException((int)CosClientError.INVALID_ARGUMENT, "uploadId is null");
            }
            if (completeMultipartUpload.parts.Count == 0)
            {
                throw new CosClientException((int)CosClientError.INVALID_ARGUMENT, "completeMultipartUpload.parts count = 0");
            }
        }

        protected override void InternalUpdateQueryParameters()
        {
            this.queryParameters.Add("uploadId", uploadId);
            base.InternalUpdateQueryParameters();
        }
    }
}
