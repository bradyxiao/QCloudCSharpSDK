using System;
using System.Collections.Generic;

using System.Text;
using COSXML.Common;

namespace COSXML.Model.Object
{
    public sealed class HeadObjectRequest : ObjectRequest
    {
        public HeadObjectRequest(string bucket, string key)
            : base(bucket, key)
        {
            this.method = CosRequestMethod.HEAD;
        }

        public void SetVersionId(string versionId)
        {
            if (versionId != null)
            {
                SetQueryParameter(CosRequestHeaderKey.VERSION_ID, versionId);
            }
        }

        public void SetIfModifiedSince(string ifModifiedSince)
        {
            if (ifModifiedSince != null)
            {
                SetRequestHeader(CosRequestHeaderKey.IF_MODIFIED_SINCE, ifModifiedSince);
            }
        }

    }
}
