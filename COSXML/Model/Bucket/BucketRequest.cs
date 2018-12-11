using System;
using System.Collections.Generic;

using System.Text;
using COSXML.CosException;
using COSXML.Common;
/**
* Copyright (c) 2018 Tencent Cloud. All rights reserved.
* 11/2/2018 8:03:59 PM
* bradyxiao
*/
namespace COSXML.Model.Bucket
{
    /**
     * Buceket request for cos
     * base class
     * provider bucket,region property
     */
    public abstract class BucketRequest : CosRequest
    {
        protected string bucket;

        protected string region;

        public BucketRequest(string bucket)
        {
            this.bucket = bucket;
            this.path = "/";
        }

        public string Bucket
        {
            get { return this.bucket; }
            set { this.bucket = value; }
        }

        public string Region
        {
            get { return this.region; }
            set { this.region = value; }
        }

        public override Network.RequestBody GetRequestBody()
        {
            return null;
        }

        public override string GetHost()
        {
            StringBuilder hostBuilder = new StringBuilder();
            if (bucket.EndsWith("-" + appid))
            {
                hostBuilder.Append(bucket);
            }
            else
            {
                hostBuilder.Append(bucket).Append("-")
                    .Append(appid);
            }
            hostBuilder.Append(".cos.")
                .Append(region)
                .Append(".myqcloud.com");
            return hostBuilder.ToString();
        }

        public override void CheckParameters()
        {
            if (bucket == null)
            {
                throw new CosClientException((int)CosClientError.INVALID_ARGUMENT, "bucket is null");
            }
            if (region == null)
            {
                throw new CosClientException((int)CosClientError.INVALID_ARGUMENT, "region is null");
            }
        }

    }
}
