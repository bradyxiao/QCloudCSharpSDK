using System;
using System.Collections.Generic;

using System.Text;

namespace COSXML.Model.Object
{
    public sealed class OptionObjectResult : CosResult
    {
        public string accessControlAllowOrigin;
        public long accessControlMaxAge;
        public List<string> accessControlAllowHeaders;
        public List<string> accessControlAllowMethods;
        public List<string> accessControlAllowExposeHeaders;

        internal override void ParseResponseBody(System.IO.Stream inputStream, string contentType, long contentLength)
        {
            List<string> values;
            this.responseHeaders.TryGetValue("Access-Control-Allow-Origin", out values);
            if (values != null && values.Count > 0)
            {
                accessControlAllowOrigin = values[0];
            }
            this.responseHeaders.TryGetValue("Access-Control-Max-Age", out values);
            if (values != null && values.Count > 0)
            {
                long.TryParse(values[0], out accessControlMaxAge);
            }
            this.responseHeaders.TryGetValue("Access-Control-Allow-Methods", out values);
            if (values != null && values.Count > 0)
            {
                accessControlAllowMethods = new List<string>(values[0].Split(','));
            }
            this.responseHeaders.TryGetValue("Access-Control-Allow-Headers", out values);
            if (values != null && values.Count > 0)
            {
                accessControlAllowHeaders = new List<string>(values[0].Split(','));
            }
            this.responseHeaders.TryGetValue("Access-Control-Expose-Headers", out values);
            if (values != null && values.Count > 0)
            {
                accessControlAllowExposeHeaders = new List<string>(values[0].Split(','));
            }
        }

        public override string GetResultInfo()
        {
            return base.GetResultInfo();
        }
    }
}
