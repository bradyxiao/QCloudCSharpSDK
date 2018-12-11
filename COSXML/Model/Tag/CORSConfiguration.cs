using System;
using System.Collections.Generic;

using System.Text;

namespace COSXML.Model.Tag
{
    public sealed class CORSConfiguration
    {
        public List<CORSRule> corsRules;

        public string GetInfo()
        {
            StringBuilder stringBuilder = new StringBuilder("{CORSConfiguration:\n");
            if(corsRules != null){
                foreach(CORSRule corsRule in corsRules){
                    if(corsRule != null) stringBuilder.Append(corsRule.GetInfo()).Append("\n");
                }
            }
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }


        public sealed class CORSRule
        {
            public string id;
            public string allowedOrigin;
            public List<string> allowedMethods;
            public List<string> allowedHeaders;
            public List<string> exposeHeaders;
            public int maxAgeSeconds;

            public string GetInfo()
            {
                StringBuilder stringBuilder = new StringBuilder("{CORSRule:\n");
                stringBuilder.Append("ID:").Append(id).Append("\n");
                stringBuilder.Append("AllowedOrigin:").Append(allowedOrigin).Append("\n");
                if(allowedMethods != null){
                    foreach (string method in allowedMethods){
                        if(method != null)stringBuilder.Append("AllowedMethod:").Append(method).Append("\n");
                    }
                }
                if(allowedHeaders != null){
                    foreach (string header in allowedHeaders){
                        if(header != null)stringBuilder.Append("AllowedHeader:").Append(header).Append("\n");
                    }
                }
                if(exposeHeaders != null){
                    foreach (string exposeHeader in exposeHeaders){
                        if (exposeHeader != null) stringBuilder.Append("ExposeHeader:").Append(exposeHeader).Append("\n");
                    }
                }
                stringBuilder.Append("MaxAgeSeconds:").Append(maxAgeSeconds).Append("\n");
                stringBuilder.Append("}");
                return stringBuilder.ToString();
            }
        }
    }
}
