using System;
using System.Collections.Generic;

using System.Text;
using COSXML.Model.Tag;
using COSXML.Common;
using COSXML.Network;

namespace COSXML.Model.Object
{
    public sealed class RestoreObjectRequest : ObjectRequest
    {
        private RestoreConfigure restoreConfigure;

        public RestoreObjectRequest(string bucket, string key)
            : base(bucket, key)
        {
            this.method = CosRequestMethod.POST;
            this.needMD5 = true;
            this.queryParameters.Add("restore", null);
            restoreConfigure = new RestoreConfigure();
            restoreConfigure.casJobParameters = new RestoreConfigure.CASJobParameters();
        }

        public void SetExpireDays(int days)
        {
            if (days < 0) days = 0;
            restoreConfigure.days = days;
        }

        public void SetTier(RestoreConfigure.Tier tier)
        {
            restoreConfigure.casJobParameters.tier = tier;
        }

        public override Network.RequestBody GetRequestBody()
        {
            string content = Transfer.XmlBuilder.BuildRestoreConfigure(restoreConfigure);
            byte[] data = Encoding.UTF8.GetBytes(content);
            ByteRequestBody body = new ByteRequestBody(data);
            return body;
        }
    }
}
