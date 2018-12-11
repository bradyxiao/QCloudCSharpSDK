using System;
using System.Collections.Generic;

using System.Text;
/**
* Copyright (c) 2018 Tencent Cloud. All rights reserved.
* 11/5/2018 11:51:33 AM
* bradyxiao
*/
namespace COSXML.Model.Tag
{
    public sealed class CompleteMultipartUpload
    {

        public List<Part> parts;

        public string GetInfo()
        {
            StringBuilder stringBuilder = new StringBuilder("{CompleteMultipartUpload:\n");
            if (parts != null)
            {
                foreach (Part part in parts)
                {
                    stringBuilder.Append(part.GetInfo());
                }
                
            }
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }

        /**
         * 本块编号 和 eTag值
         */
        public sealed class Part
        {
            public int partNumber;
            public string eTag;

            public string GetInfo()
            {
                StringBuilder stringBuilder = new StringBuilder("{Part:\n");
                stringBuilder.Append("PartNumber:").Append(partNumber).Append("\n");
                stringBuilder.Append("ETag:").Append(eTag).Append("\n");
                stringBuilder.Append("}");
                return stringBuilder.ToString();
            }
        }
    }
}
