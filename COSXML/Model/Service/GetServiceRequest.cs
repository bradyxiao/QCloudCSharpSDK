using System;
using System.Collections.Generic;

using System.Text;
using COSXML.Common;
/**
* Copyright (c) 2018 Tencent Cloud. All rights reserved.
* 11/2/2018 5:25:23 PM
* bradyxiao
*/
namespace COSXML.Model.Service
{
    /// <summary>
    /// get service api
    /// </summary>
    public sealed class GetServiceRequest : CosRequest
    {
        public GetServiceRequest()
        {
            method = CosRequestMethod.GET;
            path = "/";
        }

        public override string GetHost()
        {
            return "service.cos.myqcloud.com";
        }

        public override Network.RequestBody GetRequestBody()
        {
            return null;
        }

        public override void CheckParameters()
        {
            return;
        }
    }
}
