using System;
using System.Collections.Generic;

using System.Text;
using COSXML.Model.Tag;
using COSXML.Transfer;

namespace COSXML.Model.Bucket
{
    public sealed class GetBucketLocationResult : CosResult
    {
        public LocationConstraint locationConstraint;

        internal override void ParseResponseBody(System.IO.Stream inputStream, string contentType, long contentLength)
        {
            locationConstraint = new LocationConstraint();
            XmlParse.ParseLocationConstraint(inputStream, locationConstraint);
        }

        public override string GetResultInfo()
        {
            return base.GetResultInfo() + (locationConstraint == null ? "" : "\n" + locationConstraint.GetInfo());
        }
    }
}
