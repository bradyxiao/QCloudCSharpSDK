using System;
using System.Collections.Generic;

using System.Text;
using COSXML.Model.Tag;
using COSXML.Transfer;
/**
* Copyright (c) 2018 Tencent Cloud. All rights reserved.
* 11/2/2018 10:01:37 PM
* bradyxiao
*/
namespace COSXML.Model.Bucket
{
    public class GetBucketCORSResult : CosResult
    {
        public CORSConfiguration corsConfiguration;

        internal override void ParseResponseBody(System.IO.Stream inputStream, string contentType, long contentLength)
        {
            corsConfiguration = new CORSConfiguration();
            XmlParse.ParseCORSConfiguration(inputStream, corsConfiguration);
        }

        public override string GetResultInfo()
        {
            return base.GetResultInfo() + (corsConfiguration == null ? "" : "\n " + corsConfiguration.GetInfo());
        }

    }
}
