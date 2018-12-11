using System;
using System.Collections.Generic;

using System.Text;
using COSXML.Transfer;
using COSXML.Model.Tag;

namespace COSXML.Model.Bucket
{
    public sealed class ListMultiUploadsResult : CosResult
    {
        public ListMultipartUploads listMultipartUploads;

        internal override void ParseResponseBody(System.IO.Stream inputStream, string contentType, long contentLength)
        {
            listMultipartUploads = new ListMultipartUploads();
            XmlParse.ParseListMultipartUploads(inputStream, listMultipartUploads);
        }

        public override string GetResultInfo()
        {
            return base.GetResultInfo() + (listMultipartUploads == null ? "" : "\n" + listMultipartUploads.GetInfo());
        }
    }
}
