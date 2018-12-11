using System;
using System.Collections.Generic;

using System.Text;
/**
* Copyright (c) 2018 Tencent Cloud. All rights reserved.
* 11/5/2018 11:55:28 AM
* bradyxiao
*/
namespace COSXML.Model.Tag
{
    public sealed class CompleteMultipartUploadResult
    {
        /**
         * 创建的Object的外网访问域名
         */
        public string location;
        /**
         * 分块上传的目标Bucket
         */
        public string bucket;
        /**
         * Object的名称
         */
        public string key;
        /**
         * 合并后文件的 MD5 算法校验值
         */
        public string eTag;

        
        public string GetInfo()
        {
            StringBuilder stringBuilder = new StringBuilder("{CompleteMultipartUploadResult:\n");
            stringBuilder.Append("Location:").Append(location).Append("\n");
            stringBuilder.Append("Bucket:").Append(bucket).Append("\n");
            stringBuilder.Append("Key:").Append(key).Append("\n");
            stringBuilder.Append("ETag:").Append(eTag).Append("\n");
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }
    }
}
