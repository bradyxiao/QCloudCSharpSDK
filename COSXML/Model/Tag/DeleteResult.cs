using System;
using System.Collections.Generic;

using System.Text;
/**
* Copyright (c) 2018 Tencent Cloud. All rights reserved.
* 11/5/2018 11:42:16 AM
* bradyxiao
*/
namespace COSXML.Model.Tag
{
    public sealed class DeleteResult
    {
        public List<Deleted> deletedList;
        public List<Error> errorList;

       
        public string GetInfo()
        {
            StringBuilder stringBuilder = new StringBuilder("{DeleteResult:\n");
            if(deletedList != null)
            {
                foreach(Deleted deleted in deletedList)
                {
                    if(deleted != null)stringBuilder.Append(deleted.GetInfo()).Append("\n");
                }
            }
            if(errorList != null){
                foreach(Error error in errorList)
                {
                    if(error != null)stringBuilder.Append(error.GetInfo()).Append("\n");
                }
            }
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }

        public sealed class Deleted
        {
            public string key;
            public string versionId;
            public bool deleteMarker;
            public string  deleteMarkerVersionId;
           
            public string GetInfo()
            {
                StringBuilder stringBuilder = new StringBuilder("{Deleted:\n");
                stringBuilder.Append("Key:").Append(key).Append("\n");
                stringBuilder.Append("VersionId:").Append(versionId).Append("\n");
                stringBuilder.Append("DeleteMarker:").Append(deleteMarker).Append("\n");
                stringBuilder.Append("DeleteMarkerVersionId:").Append(deleteMarkerVersionId).Append("\n");
                stringBuilder.Append("}");
                return stringBuilder.ToString();
            }
        }

        public sealed class Error
        {
            public string key;
            public string code;
            public string message;
            public string versionId;

           
            public string GetInfo()
            {
                StringBuilder stringBuilder = new StringBuilder("{CosError:\n");
                stringBuilder.Append("Key:").Append(key).Append("\n");
                stringBuilder.Append("Code:").Append(code).Append("\n");
                stringBuilder.Append("Message:").Append(message).Append("\n");
                stringBuilder.Append("VersionId:").Append(versionId).Append("\n");
                stringBuilder.Append("}");
                return stringBuilder.ToString();
            }
        }
    }
}
