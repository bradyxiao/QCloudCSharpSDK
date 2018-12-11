using System;
using System.Collections.Generic;

using System.Text;

namespace COSXML.Model.Tag
{
    public sealed class ListBucket
    {

        public string name;
        public string encodingType;
        public string prefix;
        public string marker;
        public int maxKeys;
        public bool isTruncated;
        public string nextMarker;
        public List<Contents> contentsList;
        public List<CommonPrefixes> commonPrefixesList;
        public string delimiter;

    
        public string GetInfo()
        {
            StringBuilder stringBuilder = new StringBuilder("{ListBucket:\n");
            stringBuilder.Append("Name:").Append(name).Append("\n");
            stringBuilder.Append("Encoding-Type:").Append(encodingType).Append("\n");
            stringBuilder.Append("Prefix:").Append(prefix).Append("\n");
            stringBuilder.Append("Marker:").Append(marker).Append("\n");
            stringBuilder.Append("MaxKeys:").Append(maxKeys).Append("\n");
            stringBuilder.Append("IsTruncated:").Append(isTruncated).Append("\n");
            stringBuilder.Append("NextMarker:").Append(nextMarker).Append("\n");
            if(contentsList != null)
            {
                foreach (Contents contents in contentsList)
                {
                    if(contents != null)stringBuilder.Append(contents.GetInfo()).Append("\n");
                }
            }
            if(commonPrefixesList != null)
            {
                foreach (CommonPrefixes commonPrefixes in commonPrefixesList)
                {
                    if(commonPrefixes != null)stringBuilder.Append(commonPrefixes.GetInfo()).Append("\n");
                }
            }
            stringBuilder.Append("Delimiter:").Append(delimiter).Append("\n");
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }

        public sealed class Contents{
            public string key;
            public string lastModified;
            public string eTag;
            public long size;
            public Owner owner;
            public string storageClass;

        
            public string GetInfo()
            {
                StringBuilder stringBuilder = new StringBuilder("{Contents:\n");
                stringBuilder.Append("Key:").Append(key).Append("\n");
                stringBuilder.Append("LastModified:").Append(lastModified).Append("\n");
                stringBuilder.Append("ETag:").Append(eTag).Append("\n");
                stringBuilder.Append("Size:").Append(size).Append("\n");
                if (owner != null)stringBuilder.Append(owner.GetInfo()).Append("\n");
                stringBuilder.Append("StorageClass:").Append(storageClass).Append("\n");
                stringBuilder.Append("}");
                return stringBuilder.ToString();
            }
        }

        public sealed class CommonPrefixes{
            public string prefix;


            public string GetInfo()
            {
                StringBuilder stringBuilder = new StringBuilder("{CommonPrefixes:\n");
                stringBuilder.Append("Prefix:").Append(prefix).Append("\n");
                stringBuilder.Append("}");
                return stringBuilder.ToString();
            }
        }

        public sealed class Owner{
            public string id;

            public string GetInfo()
            {
                StringBuilder stringBuilder = new StringBuilder("{Owner:\n");
                stringBuilder.Append("Id:").Append(id).Append("\n");
                stringBuilder.Append("}");
                return stringBuilder.ToString();
            }
        }
    }
}
