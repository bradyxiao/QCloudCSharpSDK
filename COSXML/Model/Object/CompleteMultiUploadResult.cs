using System;
using System.Collections.Generic;

using System.Text;
using COSXML.Model.Tag;
using COSXML.Transfer;

namespace COSXML.Model.Object
{
    public sealed class CompleteMultiUploadResult : CosResult
    {
        public CompleteMultipartUploadResult completeMultipartUpload;

        internal override void ParseResponseBody(System.IO.Stream inputStream, string contentType, long contentLength)
        {
            completeMultipartUpload = new CompleteMultipartUploadResult();
            XmlParse.ParseCompleteMultipartUploadResult(inputStream, completeMultipartUpload);
        }

        public override string GetResultInfo()
        {
            return base.GetResultInfo() + (completeMultipartUpload == null ? "" : "\n" + completeMultipartUpload.GetInfo());
        }
    }
}
