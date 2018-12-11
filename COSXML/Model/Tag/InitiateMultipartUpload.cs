using System;
using System.Collections.Generic;

using System.Text;
/**
* Copyright (c) 2018 Tencent Cloud. All rights reserved.
* 11/5/2018 12:01:43 PM
* bradyxiao
*/
namespace COSXML.Model.Tag
{
    public sealed class InitiateMultipartUpload
    {
         /**
         * 分片上传的目标 Bucket，由用户自定义字符串和系统生成appid数字串由中划线连接而成，如：mybucket-1250000000.
         */
        public string bucket;
        /**
         * Object 的名称
         */
        public string key;
        /**
         * 在后续上传中使用的 ID
         */
        public string uploadId;

        
        public string GetInfo()
        {
            StringBuilder stringBuilder = new StringBuilder("{InitiateMultipartUpload:\n");
            stringBuilder.Append("Bucket:").Append(bucket).Append("\n");
            stringBuilder.Append("Key:").Append(key).Append("\n");
            stringBuilder.Append("UploadId:").Append(uploadId).Append("\n");
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }
    }
}
