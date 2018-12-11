using System;
using System.Collections.Generic;

using System.Text;
using COSXML.Common;
/**
* Copyright (c) 2018 Tencent Cloud. All rights reserved.
* 11/2/2018 9:59:44 PM
* bradyxiao
*/
namespace COSXML.Model.Bucket
{
    public sealed class GetBucketCORSRequest : BucketRequest
    {
        public GetBucketCORSRequest(string bucket)
            : base(bucket)
        {
            this.method = CosRequestMethod.GET;
            this.queryParameters.Add("cors", null);
        }
    }
}
