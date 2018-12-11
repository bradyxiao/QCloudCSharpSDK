using System;
using System.Collections.Generic;

using System.Text;

namespace COSXML.Model.Object
{
    public sealed class PutObjectResult : CosResult
    {
        public string eTag;

        internal override void InternalParseResponseHeaders()
        {
            List<string> values;
            this.responseHeaders.TryGetValue("ETag", out values);
            if (values != null && values.Count > 0)
            {
                eTag = values[0];
            }
        }
    }
}
