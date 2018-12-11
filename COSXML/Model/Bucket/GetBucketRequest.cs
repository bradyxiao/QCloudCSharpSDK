using System;
using System.Collections.Generic;

using System.Text;
using COSXML.Common;

namespace COSXML.Model.Bucket
{
    public sealed class GetBucketRequest : BucketRequest
    {
        public GetBucketRequest(string bucket)
            : base(bucket)
        {
            this.method = CosRequestMethod.GET;
            this.queryParameters.Add("max-keys", 1000.ToString());
        }

        public void SetPrefix(string prefix)
        {
            if (prefix != null)
            {
                SetQueryParameter("prefix", prefix);
            }
        }

        public void SetDelimiter(string delimiter)
        {
            if (delimiter != null)
            {
                SetQueryParameter("delimiter", delimiter);
            }
        }

        public void SetEncodingType(string encodingType)
        {
            if (encodingType != null)
            {
                SetQueryParameter("encoding-type", encodingType);
            }
        }

        public void SetMarker(string marker)
        {
            if (marker != null)
            {
                SetQueryParameter("marker", marker);
            }
        }

        public void SetMaxKeys(string maxKeys)
        {
            if (maxKeys != null)
            {
                SetQueryParameter("max-keys", maxKeys);
            }
        }
    }
}
