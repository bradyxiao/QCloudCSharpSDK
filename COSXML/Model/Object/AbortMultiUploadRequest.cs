using System;
using System.Collections.Generic;

using System.Text;
using COSXML.Common;
using COSXML.CosException;

namespace COSXML.Model.Object
{
    public sealed class AbortMultiUploadRequest : ObjectRequest
    {
        private string uploadId;

        public AbortMultiUploadRequest(string bucket, string key, string uploadId) : base(bucket, key)
        {
            this.uploadId = uploadId;
            this.method = CosRequestMethod.DELETE;
        }

        public void SetUploadId(string uploadId)
        {
            this.uploadId = uploadId;
        }

        public string GetUploadId()
        {
            return uploadId;
        }

        public override void CheckParameters()
        {
            base.CheckParameters();
            if (uploadId == null)
            {
                throw new CosClientException((int)CosClientError.INVALID_ARGUMENT, "uploadId is null");
            }
        }

        protected override void InternalUpdateQueryParameters()
        {
            this.queryParameters.Add("uploadId", uploadId);
        }
    }
}
