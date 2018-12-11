using System;
using System.Collections.Generic;

using System.Text;

namespace COSXML.Model.Tag
{
    public sealed class LifecycleConfiguration
    {
        public List<Rule> rules;

        public string GetInfo()
        {
            StringBuilder stringBuilder = new StringBuilder("{LifecycleConfiguration:\n");
            if(rules != null)
            {
                foreach (Rule rule in rules){
                    if(rule != null) stringBuilder.Append(rule.GetInfo()).Append("\n");
                }
            }
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }

        public sealed class Rule
        {
            public string id;
            public Filter filter;
            public string status;
            public Transition transition;
            public Expiration expiration;
            public NoncurrentVersionExpiration noncurrentVersionExpiration;
            public NoncurrentVersionTransition noncurrentVersionTransition;
            public AbortIncompleteMultiUpload abortIncompleteMultiUpload;

            public string GetInfo()
            {
                StringBuilder stringBuilder = new StringBuilder("{Rule:\n");
                stringBuilder.Append("Id:").Append(id).Append("\n");
                if (filter != null) stringBuilder.Append(filter.GetInfo()).Append("\n");
                stringBuilder.Append("Status:").Append(status).Append("\n");
                if (transition != null) stringBuilder.Append(transition.GetInfo()).Append("\n");
                if (expiration != null) stringBuilder.Append(expiration.GetInfo()).Append("\n");
                if (noncurrentVersionExpiration != null) stringBuilder.Append(noncurrentVersionExpiration.GetInfo()).Append("\n");
                if (noncurrentVersionTransition != null) stringBuilder.Append(noncurrentVersionTransition.GetInfo()).Append("\n");
                if (abortIncompleteMultiUpload != null) stringBuilder.Append(abortIncompleteMultiUpload.GetInfo()).Append("\n");
                stringBuilder.Append("}");
                return stringBuilder.ToString();
            }

        }

        public sealed class Filter
        {
            public string prefix;

            public string GetInfo()
            {
                StringBuilder stringBuilder = new StringBuilder("{Filter:\n");
                stringBuilder.Append("Prefix:").Append(prefix).Append("\n");
                stringBuilder.Append("}");
                return stringBuilder.ToString();
            }
        }

        public sealed class FilterAnd
        {
 
        }

        public sealed class Transition
        {
            public int days;
            public string date;
            public string storageClass;

            public string GetInfo()
            {
                StringBuilder stringBuilder = new StringBuilder("{Transition:\n");
                stringBuilder.Append("Days:").Append(days).Append("\n");
                stringBuilder.Append("Date:").Append(date).Append("\n");
                stringBuilder.Append("StorageClass:").Append(storageClass).Append("\n");
                stringBuilder.Append("}");
                return stringBuilder.ToString();
            }
        }

        public sealed class Expiration
        {
            public string date;
            public int days;
            public string expiredObjectDeleteMarker;

            public string GetInfo()
            {
                StringBuilder stringBuilder = new StringBuilder("{Expiration:\n");
                stringBuilder.Append("Days:").Append(days).Append("\n");
                stringBuilder.Append("Date:").Append(date).Append("\n");
                stringBuilder.Append("ExpiredObjectDeleteMarker:").Append(expiredObjectDeleteMarker).Append("\n");
                stringBuilder.Append("}");
                return stringBuilder.ToString();
            }
        }

        public sealed class NoncurrentVersionExpiration
        {
            public int noncurrentDays;

            public string GetInfo()
            {
                StringBuilder stringBuilder = new StringBuilder("{NoncurrentVersionExpiration:\n");
                stringBuilder.Append("NoncurrentDays:").Append(noncurrentDays).Append("\n");
                stringBuilder.Append("}");
                return stringBuilder.ToString();
            }
        }

        public sealed class NoncurrentVersionTransition
        {
            public int noncurrentDays;
            public string storageClass;

            public string GetInfo()
            {
                StringBuilder stringBuilder = new StringBuilder("{NoncurrentVersionTransition:\n");
                stringBuilder.Append("NoncurrentDays:").Append(noncurrentDays).Append("\n");
                stringBuilder.Append("StorageClass:").Append(storageClass).Append("\n");
                stringBuilder.Append("}");
                return stringBuilder.ToString();
            }
        }

        public sealed class AbortIncompleteMultiUpload
        {
            public int daysAfterInitiation;

            public string GetInfo()
            {
                StringBuilder stringBuilder = new StringBuilder("{AbortIncompleteMultiUpload:\n");
                stringBuilder.Append("DaysAfterInitiation:").Append(daysAfterInitiation).Append("\n");
                stringBuilder.Append("}");
                return stringBuilder.ToString();
            }
        }
    }
}
