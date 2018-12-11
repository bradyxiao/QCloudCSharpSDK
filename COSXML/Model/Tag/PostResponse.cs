using System;
using System.Collections.Generic;

using System.Text;
/**
* Copyright (c) 2018 Tencent Cloud. All rights reserved.
* 11/5/2018 12:06:59 PM
* bradyxiao
*/
namespace COSXML.Model.Tag
{
    public sealed class PostResponse
    {
        public string location;
        public string bucket;
        public string key;
        public string eTag;

        
        public string GetInfo() 
        {
            StringBuilder stringBuilder = new StringBuilder("{PostResponse:\n");
            stringBuilder.Append("Location:").Append(location).Append("\n");
            stringBuilder.Append("Bucket:").Append(bucket).Append("\n");
            stringBuilder.Append("Key:").Append(key).Append("\n");
            stringBuilder.Append("ETag:").Append(eTag).Append("\n");
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }
    }
}
