using System;
using System.Collections.Generic;

using System.Text;
/**
* Copyright (c) 2018 Tencent Cloud. All rights reserved.
* 11/5/2018 11:59:32 AM
* bradyxiao
*/
namespace COSXML.Model.Tag
{
    public sealed class CopyObject
    {
        public string eTag;
        public string lastModified;

    
        public string GetInfo()
        {
            StringBuilder stringBuilder = new StringBuilder("{CopyObject:\n");
            stringBuilder.Append("ETag:").Append(eTag).Append("\n");
            stringBuilder.Append("LastModified:").Append(lastModified).Append("\n");
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }

    }
}
