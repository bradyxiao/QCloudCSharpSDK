using System;
using System.Collections.Generic;

using System.Text;
using COSXML.Model.Tag;
using COSXML.Transfer;
using System.IO;
/**
* Copyright (c) 2018 Tencent Cloud. All rights reserved.
* 11/2/2018 5:50:56 PM
* bradyxiao
*/
namespace COSXML.Model.Service
{
    public sealed class GetServiceResult : CosResult
    {
        /// <summary>
        /// list all buckets for users
        /// </summary>
        public ListAllMyBuckets listAllMyBuckets;

        internal override void ParseResponseBody(System.IO.Stream inputStream, string contentType, long contentLength)
        {
            listAllMyBuckets = new ListAllMyBuckets();
            XmlParse.ParseListAllMyBucketsResult(inputStream, listAllMyBuckets);
        }

        public override string GetResultInfo()
        {
            return base.GetResultInfo() + (listAllMyBuckets == null ? "" : "\n" + listAllMyBuckets.GetInfo());
        }
    }
}
