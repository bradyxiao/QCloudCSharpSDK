using System;
using System.Collections.Generic;

using System.Text;
using COSXML.CosException;
using COSXML.Common;
using COSXML.Utils;
/**
* Copyright (c) 2018 Tencent Cloud. All rights reserved.
* 11/5/2018 7:54:35 PM
* bradyxiao
*/
namespace COSXML.Model.Tag
{
    public sealed class CopySourceStruct
    {
        public string appid;
        public string bucket;
        public string region;
        public string key;
        public string versionId;

        public CopySourceStruct(string appid, string bucket, string region, string key, string versionId)
        {
            this.appid = appid;
            this.bucket = bucket;
            this.region = region;
            this.key = key;
            this.versionId = versionId;
        }

        public CopySourceStruct(string appid, string bucket, string region, string key)
            : this(appid, bucket, region, key, null) { }

        public void CheckParameters()
        {
            if (bucket == null)
            {
                throw new CosClientException((int)CosClientError.INVALID_ARGUMENT, "copy source bucket = null");
            }
            if (key == null)
            {
                throw new CosClientException((int)CosClientError.INVALID_ARGUMENT, "copy source cosPath = null");
            }
            if (appid == null)
            {
                throw new CosClientException((int)CosClientError.INVALID_ARGUMENT, "copy source appid = null");
            }
            if (region == null)
            {
                throw new CosClientException((int)CosClientError.INVALID_ARGUMENT, "copy source region = null");
            }
        }

        public string GetCopySouce()
        {
            if (!key.StartsWith("/"))
            {
                key = "/" + key;
            }
            //key = URLEncodeUtils.EncodeURL(key);
            StringBuilder copySource = new StringBuilder();

            copySource.Append(bucket);
            if (!bucket.EndsWith("-" + appid))
            {
                copySource.Append("-")
                        .Append(appid);
            }
            copySource.Append(".").Append("cos").Append(".")
                .Append(region).Append(".")
                .Append("myqcloud.com")
                .Append(key);
           
            if (versionId != null)
            {
                copySource.Append("?versionId=").Append(versionId);
            }
            return copySource.ToString();
        }
    }
}
