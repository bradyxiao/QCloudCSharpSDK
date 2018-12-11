using System;
using System.Collections.Generic;

using System.Text;
using COSXML.Common;
using COSXML.Model.Tag;
using COSXML.CosException;

namespace COSXML.Model.Object
{
    public sealed class UploadPartCopyRequest : ObjectRequest
    {
        private CopySourceStruct copySourceStruct;
        /**Specified part number*/
        private int partNumber = -1;
        /**init upload generate' s uploadId by service*/
        private String uploadId = null;

        public UploadPartCopyRequest(string bucket, string key, int partNumber, string uploadId)
            : base(bucket, key)
        {
            this.method = CosRequestMethod.PUT;
            this.partNumber = partNumber;
            this.uploadId = uploadId;
        }

        public void SetCopySource(CopySourceStruct copySource)
        {
            this.copySourceStruct = copySource;
        }

        public void SetCopyRange(long start, long end)
        {
            if (start >= 0 && end >= start)
            {
                string bytes = String.Format("bytes={0}-{1}", start, end);
                SetRequestHeader(CosRequestHeaderKey.X_COS_COPY_SOURCE_RANGE, bytes);
            }
        }

        public void SetCopyIfModifiedSince(string sourceIfModifiedSince)
        {
            if (sourceIfModifiedSince != null)
            {
                SetRequestHeader(CosRequestHeaderKey.X_COS_COPY_SOURCE_IF_MODIFIED_SINCE, sourceIfModifiedSince);
            }
        }

        public void SsetCopyIfUnmodifiedSince(string sourceIfUnmodifiedSince)
        {
            if (sourceIfUnmodifiedSince != null)
            {
                SetRequestHeader(CosRequestHeaderKey.X_COS_COPY_SOURCE_IF_UNMODIFIED_SINCE, sourceIfUnmodifiedSince);
            }
        }

        public void SetCopyIfMatch(string eTag)
        {
            if (eTag != null)
            {
                SetRequestHeader(CosRequestHeaderKey.X_COS_COPY_SOURCE_IF_MATCH, eTag);
            }
        }

        public void SetCopyIfNoneMatch(string eTag)
        {
            if (eTag != null)
            {
                SetRequestHeader(CosRequestHeaderKey.X_COS_COPY_SOURCE_IF_NONE_MATCH, eTag);
            }
        }

        public override void CheckParameters()
        {
            base.CheckParameters();
            if (copySourceStruct == null)
            {
                throw new CosClientException((int)CosClientError.INVALID_ARGUMENT, "copy source = null");
            }
            else
            {
                copySourceStruct.CheckParameters();
            }
            if (partNumber <= 0)
            {
                throw new CosClientException((int)CosClientError.INVALID_ARGUMENT, "partNumber < 1");
            }
            if (uploadId == null)
            {
                throw new CosClientException((int)CosClientError.INVALID_ARGUMENT, "uploadID = null");
            }

        }

        protected override void InternalUpdateQueryParameters()
        {
            this.queryParameters.Add("partNumber", partNumber.ToString());
            this.queryParameters.Add("uploadId", uploadId);
        }

        protected override void InteranlUpdateHeaders()
        {
            this.headers.Add(CosRequestHeaderKey.X_COS_COPY_SOURCE, copySourceStruct.GetCopySouce());
        }

    }
}
