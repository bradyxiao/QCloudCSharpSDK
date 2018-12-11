using System;
using System.Collections.Generic;
using System.Text;
using COSXML.Common;
/**
* Copyright (c) 2018 Tencent Cloud. All rights reserved.
* 11/12/2018 9:22:06 PM
* bradyxiao
*/
namespace COSXML.Model.Bucket
{
    public sealed class DeleteBucketPolicyRequest : BucketRequest
    {
        public DeleteBucketPolicyRequest(string bucket)
            : base(bucket)
        {
            this.method = CosRequestMethod.DELETE;
            this.queryParameters.Add("policy ", null);
        }
    }
}
