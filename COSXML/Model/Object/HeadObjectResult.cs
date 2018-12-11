using System;
using System.Collections.Generic;

using System.Text;

namespace COSXML.Model.Object
{
    public sealed class HeadObjectResult : CosResult
    {
        public string cosObjectType;
        public string cosStorageClass;
        public long size;
        public string eTag;

        internal override void InternalParseResponseHeaders()
        {
            List<string> values = new List<string>();
            this.responseHeaders.TryGetValue("x-cos-object-type", out values);
            if (values != null && values.Count > 0)
            {
                cosObjectType = values[0];
            }
            this.responseHeaders.TryGetValue("x-cos-storage-class", out values);
            if (values != null && values.Count > 0)
            {
                cosObjectType = values[0];
            }
            this.responseHeaders.TryGetValue("Content-Length", out values);
            if (values != null && values.Count > 0)
            {
                long.TryParse(values[0], out size);
            }
            this.responseHeaders.TryGetValue("ETag", out values);
            if (values != null && values.Count > 0)
            {
                eTag = values[0];
            }
        }
    }
}
