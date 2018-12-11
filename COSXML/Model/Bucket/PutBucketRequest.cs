using System;
using System.Collections.Generic;

using System.Text;
using COSXML.Common;
using COSXML.Utils;
using COSXML.Model.Tag;



namespace COSXML.Model.Bucket
{
    public sealed class PutBucketRequest : BucketRequest
    {
        public PutBucketRequest(string bucket)
            : base(bucket)
        {
            this.method = CosRequestMethod.PUT;
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
