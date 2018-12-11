using System;
using System.Collections.Generic;

using System.Text;
/**
* Copyright (c) 2018 Tencent Cloud. All rights reserved.
* 11/5/2018 12:13:21 PM
* bradyxiao
*/
namespace COSXML.Model.Tag
{
    public sealed class CosServerError
    {
        public string code;
        public string message;
        public string resource;
        public string requestId;
        public string traceId;

        public string GetInfo()
        {
            StringBuilder stringBuilder = new StringBuilder("{Error:\n");
            stringBuilder.Append("Code:").Append(code).Append("\n");
            stringBuilder.Append("Message:").Append(message).Append("\n");
            stringBuilder.Append("Rresource:").Append(resource).Append("\n");
            stringBuilder.Append("RequestId:").Append(requestId).Append("\n");
            stringBuilder.Append("TraceId:").Append(traceId).Append("\n");
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }
    }
}
