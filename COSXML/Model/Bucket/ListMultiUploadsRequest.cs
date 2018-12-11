using System;
using System.Collections.Generic;

using System.Text;
using COSXML.Common;

namespace COSXML.Model.Bucket
{
    public sealed class ListMultiUploadsRequest : BucketRequest
    {
        public ListMultiUploadsRequest(string bucket)
            : base(bucket)
        {
            this.bucket = bucket;
            this.method = CosRequestMethod.GET;
            this.queryParameters.Add("uploads", null);
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
                SetQueryParameter("Encoding-type", encodingType);
            }
        }

        public void SetKeyMarker(string keyMarker)
        {
            if (keyMarker != null)
            {
                SetQueryParameter("key-marker", keyMarker);
            }
        }

        public void SetMaxUploads(string maxUploads)
        {
            if (maxUploads != null)
            {
                SetQueryParameter("max-uploads", maxUploads);
            }
        }

        public void SetPrefix(string prefix)
        {
            if (prefix != null)
            {
                SetQueryParameter("Prefix", prefix);
            }
        }

        public void SetUploadIdMarker(string uploadIdMarker)
        {
            if (uploadIdMarker != null)
            {
                SetQueryParameter("upload-id-marker", uploadIdMarker);
            }
        }
    }
}
