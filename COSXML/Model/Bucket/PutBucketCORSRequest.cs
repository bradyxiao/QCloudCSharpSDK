using System;
using System.Collections.Generic;

using System.Text;
using COSXML.Common;
using COSXML.Model.Tag;
using COSXML.Network;
using COSXML.CosException;

namespace COSXML.Model.Bucket
{
    public sealed class PutBucketCORSRequest : BucketRequest
    {
        private CORSConfiguration corsConfiguration;

        public PutBucketCORSRequest(string bucket)
            : base(bucket)
        {
            this.method = CosRequestMethod.PUT;
            this.queryParameters.Add("cors", null);
            corsConfiguration = new CORSConfiguration();
            corsConfiguration.corsRules = new List<CORSConfiguration.CORSRule>();
            this.needMD5 = true;
        }

        public override Network.RequestBody GetRequestBody()
        {
            string content = Transfer.XmlBuilder.BuildCORSConfigXML(corsConfiguration);
            byte[] data = Encoding.UTF8.GetBytes(content);
            ByteRequestBody body = new ByteRequestBody(data);
            return body;
        }

        public void SetCORSRule(CORSConfiguration.CORSRule corsRule)
        {
            if (corsRule != null)
            {
                corsConfiguration.corsRules.Add(corsRule);
            }
        }

        public void SetCORSRules(List<CORSConfiguration.CORSRule> corsRules)
        {
            if (corsRules != null)
            {
                corsConfiguration.corsRules.AddRange(corsRules);
            }
        }

        public override void CheckParameters()
        {
            base.CheckParameters();
            if (corsConfiguration.corsRules.Count == 0) throw new CosClientException((int)CosClientError.INVALID_ARGUMENT, "corsConfiguration.corsRules.Count = 0");
        }
    }
}
