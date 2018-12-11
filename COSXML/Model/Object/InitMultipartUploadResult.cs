using System;
using System.Collections.Generic;

using System.Text;
using COSXML.Model.Tag;
using COSXML.Transfer;

namespace COSXML.Model.Object
{
    public sealed class InitMultipartUploadResult : CosResult
    {
        public InitiateMultipartUpload initMultipartUpload;

        internal override void ParseResponseBody(System.IO.Stream inputStream, string contentType, long contentLength)
        {
            initMultipartUpload = new InitiateMultipartUpload();
            XmlParse.ParseInitiateMultipartUpload(inputStream, initMultipartUpload);
        }

        public override string GetResultInfo()
        {
            return base.GetResultInfo() + (initMultipartUpload == null ? "" : "\n" + initMultipartUpload.GetInfo());
        }
    }
}
