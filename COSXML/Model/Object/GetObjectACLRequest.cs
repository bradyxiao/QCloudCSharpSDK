using System;
using System.Collections.Generic;

using System.Text;
using COSXML.Common;

namespace COSXML.Model.Object
{
    public sealed class GetObjectACLRequest : ObjectRequest
    {
        public GetObjectACLRequest(string bucket, string key)
            : base(bucket, key)
        {
            this.method = CosRequestMethod.GET;
            this.queryParameters.Add("acl", null);
        }
    }
}
