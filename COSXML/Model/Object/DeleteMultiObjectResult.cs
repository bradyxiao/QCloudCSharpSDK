using System;
using System.Collections.Generic;

using System.Text;
using COSXML.Model.Tag;
using COSXML.Transfer;

namespace COSXML.Model.Object
{
    public sealed class DeleteMultiObjectResult : CosResult
    {
        public DeleteResult deleteResult;

        internal override void ParseResponseBody(System.IO.Stream inputStream, string contentType, long contentLength)
        {
            deleteResult = new DeleteResult();
            XmlParse.ParseDeleteResult(inputStream, deleteResult);
        }

        public override string GetResultInfo()
        {
            return base.GetResultInfo() + (deleteResult == null ? "" : deleteResult.GetInfo());
        }
    }
}
