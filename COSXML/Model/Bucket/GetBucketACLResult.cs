using System;
using System.Collections.Generic;

using System.Text;
using COSXML.Model.Tag;
using System.IO;
using COSXML.Transfer;
/**
* Copyright (c) 2018 Tencent Cloud. All rights reserved.
* 11/2/2018 9:08:55 PM
* bradyxiao
*/
namespace COSXML.Model.Bucket
{
    public sealed class GetBucketACLResult : CosResult
    {
        public AccessControlPolicy accessControlPolicy;

        internal override void ParseResponseBody(Stream inputStream, string contentType, long contentLength)
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
