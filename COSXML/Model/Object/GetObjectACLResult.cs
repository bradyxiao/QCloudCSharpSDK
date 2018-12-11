using System;
using System.Collections.Generic;

using System.Text;
using COSXML.Model.Tag;
using COSXML.Transfer;

namespace COSXML.Model.Object
{
    public sealed class GetObjectACLResult : CosResult
    {
        public AccessControlPolicy accessControlPolicy;

        internal override void ParseResponseBody(System.IO.Stream inputStream, string contentType, long contentLength)
        {
            accessControlPolicy = new AccessControlPolicy();
            XmlParse.ParseAccessControlPolicy(inputStream, accessControlPolicy);
        }

        public override string GetResultInfo()
        {
            return base.GetResultInfo() + (accessControlPolicy == null ? "" : "\n" + accessControlPolicy.GetInfo());
        }
    }
}
