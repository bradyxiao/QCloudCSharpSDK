using System;
using System.Collections.Generic;

using System.Text;
using COSXML.Common;
/**
* Copyright (c) 2018 Tencent Cloud. All rights reserved.
* 11/2/2018 9:05:42 PM
* bradyxiao
*/
namespace COSXML.Model.Bucket
{
    public sealed class DeleteBucketTaggingRequest : BucketRequest
    {
        public DeleteBucketTaggingRequest(string bucket)
            : base(bucket)
        {
            this.method = CosRequestMethod.DELETE;
            this.queryParameters.Add("tagging", null);
        }
    }
}
