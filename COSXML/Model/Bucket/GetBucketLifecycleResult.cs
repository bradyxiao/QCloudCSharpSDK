using System;
using System.Collections.Generic;

using System.Text;
using COSXML.Model.Tag;
using COSXML.Transfer;

namespace COSXML.Model.Bucket
{
    public sealed class GetBucketLifecycleResult : CosResult
    {
        public LifecycleConfiguration lifecycleConfiguration;

        internal override void ParseResponseBody(System.IO.Stream inputStream, string contentType, long contentLength)
        {
            lifecycleConfiguration = new LifecycleConfiguration();
            XmlParse.ParseLifecycleConfiguration(inputStream, lifecycleConfiguration);
        }

        public override string GetResultInfo()
        {
            return base.GetResultInfo() + (lifecycleConfiguration == null ? "" : "\n " + lifecycleConfiguration.GetInfo());
        }
    }
}
