using System;
using System.Collections.Generic;

using System.Text;
/**
* Copyright (c) 2018 Tencent Cloud. All rights reserved.
* 11/2/2018 9:48:29 PM
* bradyxiao
*/
namespace COSXML.Model.Tag
{
    public sealed class AccessControlPolicy
    {
        public Owner owner;
        public AccessControlList accessControlList;

        public string GetInfo()
        {
            StringBuilder stringBuilder = new StringBuilder("{AccessControlPolicy:\n");
            if (owner != null) stringBuilder.Append(owner.GetInfo()).Append("\n");
            if (accessControlList != null) stringBuilder.Append(accessControlList.GetInfo()).Append("\n");
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }

        public sealed class Owner
        {
            public string id;
            public string displayName;

            public string GetInfo()
            {
                StringBuilder stringBuilder = new StringBuilder("{Owner:\n");
                stringBuilder.Append("Id:").Append(id).Append("\n");
                stringBuilder.Append("DisplayName:").Append(displayName).Append("\n");
                stringBuilder.Append("}");
                return stringBuilder.ToString();
            }
        }


        public sealed class AccessControlList
        {
            public List<Grant> grants;

            public string GetInfo()
            {
                StringBuilder stringBuilder = new StringBuilder("{AccessControlList:\n");
                if (grants != null)
                {
                    foreach (Grant grant in grants)
                    {
                        if (grant != null) stringBuilder.Append(grant.GetInfo()).Append("\n");
                    }
                }
                stringBuilder.Append("}");
                return stringBuilder.ToString();
            }
        }

        public sealed class Grant
        {
            public Grantee grantee;
            public string permission;

            public string GetInfo()
            {
                StringBuilder stringBuilder = new StringBuilder("{Grant:\n");
                if (grantee != null) stringBuilder.Append(grantee.GetInfo()).Append("\n");
                stringBuilder.Append("Permission:").Append(permission).Append("\n");
                stringBuilder.Append("}");
                return stringBuilder.ToString();
            }
        }

        public sealed class Grantee
        {
            public string id;
            public string displayName;
            public string uri;

            public string GetInfo()
            {
                StringBuilder stringBuilder = new StringBuilder("{Grantee:\n");
                if (uri != null) stringBuilder.Append("URI:").Append(uri).Append("\n");
                if(id != null) stringBuilder.Append("Id:").Append(id).Append("\n");
                if(displayName != null) stringBuilder.Append("DisplayName:").Append(displayName).Append("\n");
                stringBuilder.Append("}");
                return stringBuilder.ToString();
            }
        }
    }

    
}
