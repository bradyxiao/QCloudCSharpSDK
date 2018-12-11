using System;
using System.Collections.Generic;

using System.Text;
using COSXML.Model.Tag;
using COSXML.Transfer;

namespace COSXML.Model.Object
{
    public sealed class ListPartsResult : CosResult
    {
        public ListParts listParts;

        internal override void ParseResponseBody(System.IO.Stream inputStream, string contentType, long contentLength)
        {
            listParts = new ListParts();
            XmlParse.ParseListParts(inputStream, listParts);
        }

        public override string GetResultInfo()
        {
            return base.GetResultInfo() + (listParts == null ? "" : "\n" + listParts.GetInfo());
        }

    }
}
