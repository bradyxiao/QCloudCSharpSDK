using System;
using System.Collections.Generic;

using System.Text;
/**
* Copyright (c) 2018 Tencent Cloud. All rights reserved.
* 11/2/2018 5:52:22 PM
* bradyxiao
*/
namespace COSXML.Model.Tag
{
    public sealed class ListAllMyBuckets
    {
        public Owner owner;
        public List<Bucket> buckets;

        public string GetInfo()
        {
            StringBuilder stringBuilder = new StringBuilder("{ListAllMyBuckets:\n");
            if (owner != null) stringBuilder.Append(owner.GetInfo()).Append("\n");
            stringBuilder.Append("Buckets:\n");
            if (buckets != null)
            {
                foreach (Bucket bucket in buckets)
                {
                    if (bucket != null) stringBuilder.Append(bucket.GetInfo()).Append("\n");
                }
            }
            stringBuilder.Append("}").Append("\n");
            stringBuilder.Append("}");
            return stringBuilder.ToString();
         }

        public sealed class Owner
        {
            public string id;
            public string disPlayName;

            public string GetInfo()
            {
                StringBuilder stringBuilder = new StringBuilder("{Owner:\n");
                stringBuilder.Append("ID:").Append(id).Append("\n");
                stringBuilder.Append("DisPlayName:").Append(disPlayName).Append("\n");
                stringBuilder.Append("}");
                return stringBuilder.ToString();
            }
        }

        public sealed class Bucket
        {
            public string name;
            public string location;
            public string createDate;
            public string GetInfo()
            {
                StringBuilder stringBuilder = new StringBuilder("{Bucket:\n");
                stringBuilder.Append("Name:").Append(name).Append("\n");
                stringBuilder.Append("Location:").Append(location).Append("\n");
                stringBuilder.Append("CreateDate:").Append(createDate).Append("\n");
                stringBuilder.Append("}");
                return stringBuilder.ToString();
            }
        }
    }

    
}
