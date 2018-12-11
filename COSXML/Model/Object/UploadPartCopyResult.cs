using System;
using System.Collections.Generic;

using System.Text;
using COSXML.Model.Tag;
using COSXML.Transfer;

namespace COSXML.Model.Object
{
    public sealed class UploadPartCopyResult : CosResult
    {
        public CopyObject copyObject;

        internal override void ParseResponseBody(System.IO.Stream inputStream, string contentType, long contentLength)
        {
            copyObject = new CopyObject();
            XmlParse.ParseCopyObjectResult(inputStream, copyObject);
        }

        public override string GetResultInfo()
        {
            return base.GetResultInfo() + (copyObject == null ? "" : "\n" + copyObject.GetInfo());
        }
    }
}
