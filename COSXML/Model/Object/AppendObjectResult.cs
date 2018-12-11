using System;
using System.Collections.Generic;

using System.Text;

namespace COSXML.Model.Object
{
    public sealed class AppendObjectResult : CosResult
    {
        private string contentSHA1;
        private string nextAppendPosition;

        internal override void InternalParseResponseHeaders()
        {
            List<string> values;
            this.responseHeaders.TryGetValue("x-cos-content-sha1", out values);
            if (values != null && values.Count > 0)
            {
                contentSHA1 = values[0];
            }
            this.responseHeaders.TryGetValue("x-cos-next-append-position", out values);
            if (values != null && values.Count > 0)
            {
                nextAppendPosition = values[0];
            }
        }
    }
}
