using System;
using System.Collections.Generic;

using System.Text;

namespace COSXML.Model.Tag
{
    public sealed class ListMultipartUploads
    {

        public string bucket;
        public string encodingType;
        public string keyMarker;
        public string uploadIdMarker;
        public string nextKeyMarker;
        public string nextUploadIdMarker;
        public string maxUploads;
        public bool isTruncated;
        public string prefix;
        public string delimiter;
        public List<Upload> uploads;
        public List<CommonPrefixes> commonPrefixes;


        public string GetInfo()
        {
            StringBuilder stringBuilder = new StringBuilder("{ListMultipartUploads:\n");
            stringBuilder.Append("Bucket:").Append(bucket).Append("\n");
            stringBuilder.Append("Encoding-Type:").Append(encodingType).Append("\n");
            stringBuilder.Append("KeyMarker:").Append(keyMarker).Append("\n");
            stringBuilder.Append("UploadIdMarker:").Append(uploadIdMarker).Append("\n");
            stringBuilder.Append("NextKeyMarker:").Append(nextKeyMarker).Append("\n");
            stringBuilder.Append("NextUploadIdMarker:").Append(nextUploadIdMarker).Append("\n");
            stringBuilder.Append("MaxUploads:").Append(maxUploads).Append("\n");
            stringBuilder.Append("IsTruncated:").Append(isTruncated).Append("\n");
            stringBuilder.Append("Prefix:").Append(prefix).Append("\n");
            stringBuilder.Append("Delimiter:").Append(delimiter).Append("\n");
            if (uploads != null)
            {
                foreach (Upload upload in uploads)
                {
                    if (upload != null) stringBuilder.Append(upload.GetInfo()).Append("\n");
                }
            }
            if (commonPrefixes != null)
            {
                foreach (CommonPrefixes commonPrefix in commonPrefixes)
                {
                    if (commonPrefix != null) stringBuilder.Append(commonPrefix.GetInfo()).Append("\n");
                }
            }
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }

        public sealed class Upload
        {
            public string key;
            public string uploadID;
            public string storageClass;
            public Initiator initiator;
            public Owner owner;
            public string initiated;


            public string GetInfo()
            {
                StringBuilder stringBuilder = new StringBuilder("{Upload:\n");
                stringBuilder.Append("Key:").Append(key).Append("\n");
                stringBuilder.Append("UploadID:").Append(uploadID).Append("\n");
                stringBuilder.Append("StorageClass:").Append(storageClass).Append("\n");
                if (initiator != null) stringBuilder.Append(initiator.GetInfo()).Append("\n");
                if (owner != null) stringBuilder.Append(owner.GetInfo()).Append("\n");
                stringBuilder.Append("Initiated:").Append(initiated).Append("\n");
                stringBuilder.Append("}");
                return stringBuilder.ToString();
            }
        }

        public sealed class CommonPrefixes
        {
            public string prefix;

            public string GetInfo()
            {
                StringBuilder stringBuilder = new StringBuilder("{CommonPrefixes:\n");
                stringBuilder.Append("Prefix:").Append(prefix).Append("\n");
                stringBuilder.Append("}");
                return stringBuilder.ToString();
            }
        }

        public sealed class Initiator
        {
            public string uin;
            public string id;
            public string displayName;

            public string GetInfo()
            {
                StringBuilder stringBuilder = new StringBuilder("{Initiator:\n");
                stringBuilder.Append("Uin:").Append(uin).Append("\n");
                stringBuilder.Append("Id:").Append(id).Append("\n");
                stringBuilder.Append("DisplayName:").Append(displayName).Append("\n");
                stringBuilder.Append("}");
                return stringBuilder.ToString();
            }
        }

        public sealed class Owner
        {
            public string uid;
            public string id;
            public string displayName;

            public string GetInfo()
            {
                StringBuilder stringBuilder = new StringBuilder("{Owner:\n");
                stringBuilder.Append("Uid:").Append(uid).Append("\n");
                stringBuilder.Append("Id:").Append(id).Append("\n");
                stringBuilder.Append("DisplayName:").Append(displayName).Append("\n");
                stringBuilder.Append("}");
                return stringBuilder.ToString();
            }
        }
    }
}
