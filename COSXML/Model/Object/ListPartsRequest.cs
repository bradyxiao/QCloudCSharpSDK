using System;
using System.Collections.Generic;

using System.Text;
using COSXML.Common;
using COSXML.CosException;

namespace COSXML.Model.Object
{
    public sealed class ListPartsRequest : ObjectRequest
    {
        private string uploadId;

        public ListPartsRequest(string bucket, string key, string uploadId)
            : base(bucket, key)
        {
            this.method = CosRequestMethod.GET;
            this.uploadId = uploadId;
        }

        public void SetUploadId(string uploadId)
        {
            this.uploadId = uploadId;
        }

        public void SetMaxParts(int maxParts)
        {
            SetQueryParameter(CosRequestHeaderKey.MAX_PARTS, maxParts.ToString());
        }

        public void SetPartNumberMarker(int partNumberMarker)
        {
           SetQueryParameter(CosRequestHeaderKey.PART_NUMBER_MARKER, partNumberMarker.ToString());
        }

        public void SetEncodingType(string encodingType)
        {
            SetQueryParameter(CosRequestHeaderKey.ENCODING_TYPE, encodingType);
        }

        public override void CheckParameters()
        {
            base.CheckParameters();
            if (uploadId == null)
            {
                throw new CosClientException((int)CosClientError.INVALID_ARGUMENT, "uploadId = null");
            }
        }

        protected override void InternalUpdateQueryParameters()
        {
            queryParameters.Add("uploadId", uploadId);
        }


    }
}
