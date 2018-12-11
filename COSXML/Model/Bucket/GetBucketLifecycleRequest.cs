using System;
using System.Collections.Generic;

using System.Text;
using COSXML.Common;

namespace COSXML.Model.Bucket
{
    public sealed class GetBucketLifecycleRequest : BucketRequest
    {
        public GetBucketLifecycleRequest(string bucket)
            : base(bucket)
        {
            this.method = CosRequestMethod.GET;
            this.queryParameters.Add("lifecycle", null);
        }
    }
}
