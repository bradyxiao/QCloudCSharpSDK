using System;
using System.Collections.Generic;

using System.Text;
using COSXML.Model.Tag;
using COSXML.Transfer;
using System.IO;

namespace COSXML.Model.Bucket
{
    public sealed class GetBucketResult : CosResult
    {
        public ListBucket listBucket;

        internal override void ParseResponseBody(System.IO.Stream inputStream, string contentType, long contentLength)
        {
            listBucket = new ListBucket();
            XmlParse.ParseListBucket(inputStream, listBucket);
        }

        public override string GetResultInfo()
        {
            return base.GetResultInfo() + (listBucket == null ? "" : "\n" + listBucket.GetInfo()); 
        }
    }
}
