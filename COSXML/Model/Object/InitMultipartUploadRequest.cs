using System;
using System.Collections.Generic;

using System.Text;
using COSXML.Common;
using COSXML.Utils;
using COSXML.Model.Tag;

namespace COSXML.Model.Object
{
    public sealed class InitMultipartUploadRequest : ObjectRequest
    {
        public InitMultipartUploadRequest(string bucket, string key)
            : base(bucket, key)
        {
            this.method = CosRequestMethod.POST;
            this.queryParameters.Add("uploads", null);
        }

        public void SetCacheControl(string cacheControl)
        {
            if (cacheControl != null)
            {
                SetRequestHeader(CosRequestHeaderKey.CACHE_CONTROL, cacheControl);
            }
        }

        public void SetContentDisposition(string contentDisposition)
        {
            if (contentDisposition != null)
            {
                SetRequestHeader(CosRequestHeaderKey.CONTENT_DISPOSITION, contentDisposition);
            }
        }

        public void SetContentEncoding(string contentEncoding)
        {
            if (contentEncoding != null)
            {
                SetRequestHeader(CosRequestHeaderKey.CONTENT_ENCODING, contentEncoding);
            }
        }

        public void SetExpires(string expires)
        {
            if (expires != null)
            {
                SetRequestHeader(CosRequestHeaderKey.EXPIRES, expires);
            }
        }


        public void SetCosACL(string cosACL)
        {
            if (cosACL != null)
            {
                SetRequestHeader(CosRequestHeaderKey.X_COS_ACL, cosACL);
            }
        }

        public void SetCosACL(CosACL cosACL)
        {
            SetRequestHeader(CosRequestHeaderKey.X_COS_ACL, EnumUtils.GetValue(cosACL));
        }

        public void setXCosGrantRead(GrantAccount grantAccount)
        {
            if (grantAccount != null)
            {
                SetRequestHeader(CosRequestHeaderKey.X_COS_GRANT_READ, grantAccount.GetGrantAccounts());
            }
        }

        public void setXCosGrantWrite(GrantAccount grantAccount)
        {
            if (grantAccount != null)
            {
                SetRequestHeader(CosRequestHeaderKey.X_COS_GRANT_WRITE, grantAccount.GetGrantAccounts());
            }
        }

        public void setXCosReadWrite(GrantAccount grantAccount)
        {
            if (grantAccount != null)
            {
                SetRequestHeader(CosRequestHeaderKey.X_COS_GRANT_FULL_CONTROL, grantAccount.GetGrantAccounts());
            }
        }
    }
}
