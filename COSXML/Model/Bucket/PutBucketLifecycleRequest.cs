using System;
using System.Collections.Generic;

using System.Text;
using COSXML.Common;
using COSXML.Model.Tag;
using COSXML.Network;
using COSXML.CosException;

namespace COSXML.Model.Bucket
{
    public sealed class PutBucketLifecycleRequest : BucketRequest
    {
        private LifecycleConfiguration lifecycleConfiguration;

        public PutBucketLifecycleRequest(string bucket)
            : base(bucket)
        {
            this.method = CosRequestMethod.PUT;
            this.queryParameters.Add("lifecycle", null);
            lifecycleConfiguration = new LifecycleConfiguration();
            lifecycleConfiguration.rules = new List<LifecycleConfiguration.Rule>();
            this.needMD5 = true;
        }

        public override Network.RequestBody GetRequestBody()
        {
            string content = Transfer.XmlBuilder.BuildLifecycleConfiguration(lifecycleConfiguration);
            byte[] data = Encoding.UTF8.GetBytes(content);
            ByteRequestBody body = new ByteRequestBody(data);
            return body;
        }

        public void SetRule(LifecycleConfiguration.Rule rule)
        {
            if(rule != null)
            {
                lifecycleConfiguration.rules.Add(rule);
            }
        }

        public void SetRules(List<LifecycleConfiguration.Rule> rules)
        {
            if (rules != null)
            {
                lifecycleConfiguration.rules.AddRange(rules);
            }
        }

        public override void CheckParameters()
        {
            base.CheckParameters();
            if (lifecycleConfiguration.rules.Count == 0) throw new CosClientException((int)CosClientError.INVALID_ARGUMENT, "lifecycleConfiguration.rules.Count = 0");
        }
    }
}
