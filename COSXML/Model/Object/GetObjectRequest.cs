using System;
using System.Collections.Generic;

using System.Text;
using COSXML.Common;
using COSXML.CosException;
using System.IO;

namespace COSXML.Model.Object
{
    public sealed class GetObjectRequest : ObjectRequest
    {
        private string localDir;
        private string localFileName;
        private long localFileOffset = 0;

        private COSXML.Callback.OnProgressCallback progressCallback;

        
        public GetObjectRequest(string bucket, string key, string localDir, string localFileName)
            : base(bucket, key)
        {
            this.method = CosRequestMethod.GET;
            this.localDir = localDir;
            this.localFileName = localFileName;
        }

        public void SetCosProgressCallback(COSXML.Callback.OnProgressCallback progressCallback)
        {
            this.progressCallback = progressCallback;
        }

        internal COSXML.Callback.OnProgressCallback GetCosProgressCallback()
        {
            return progressCallback;
        }

        public void SetLocalFileOffset(long localFileOffset)
        {
            this.localFileOffset = localFileOffset > 0 ? localFileOffset : 0;
        }

        public long GetLocalFileOffset()
        {
            return localFileOffset;
        }

        public void SetRange(long start, long end)
        {
            if (start < 0) return;
            if (end < start) end = -1;
            SetRequestHeader(CosRequestHeaderKey.RANGE, String.Format("bytes={0}-{1}", start, 
                (end == -1 ? "" : end.ToString()))); 

        }

        public void SetRange(long start)
        {
            SetRange(start, -1);
        }

        public void SetVersionId(string versionId)
        {
            if (versionId != null)
            {
                SetQueryParameter(CosRequestHeaderKey.VERSION_ID, versionId);
            }
        }

        public void SetResponseContentType(string responseContentType)
        {
            if (responseContentType != null)
            {
                SetQueryParameter(CosRequestHeaderKey.RESPONSE_CONTENT_TYPE, responseContentType);
            }
        }

        public void SetResponseContentLanguage(string responseContentLanguage)
        {
            if (responseContentLanguage != null)
            {
                SetQueryParameter(CosRequestHeaderKey.RESPONSE_CONTENT_LANGUAGE, responseContentLanguage);
            }
        }

        public void SetResponseCacheControl(string responseCacheControl)
        {
            if (responseCacheControl != null)
            {
                SetQueryParameter(CosRequestHeaderKey.RESPONSE_CACHE_CONTROL, responseCacheControl);
            }
        }

        public void SetResponseContentDisposition(string responseDisposition)
        {
            if (responseDisposition != null)
            {
                SetQueryParameter(CosRequestHeaderKey.RESPONSE_CONTENT_DISPOSITION, responseDisposition);
            }
        }

        public void SetResponseContentEncoding(string responseContentEncoding)
        {
            if (responseContentEncoding != null)
            {
                SetQueryParameter(CosRequestHeaderKey.RESPONSE_CONTENT_ENCODING, responseContentEncoding);
            }
        }

        public void SetResponseExpires(string responseExpires)
        {
            if (responseExpires != null)
            {
                SetQueryParameter(CosRequestHeaderKey.RESPONSE_EXPIRES, responseExpires);
            }
        }

        public override void CheckParameters()
        {
            base.CheckParameters();
            if (localDir == null) throw new CosClientException((int)CosClientError.INVALID_ARGUMENT, "localDir = null");
        }

        public string GetSaveFilePath()
        {
            string result = localDir;
            DirectoryInfo dirInfo = new DirectoryInfo(localDir);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            if (String.IsNullOrEmpty(localFileName)) localFileName = path;
            if (localDir.EndsWith(System.IO.Path.DirectorySeparatorChar.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                result = result + localFileName;
            }
            else
            {
                result = result + System.IO.Path.DirectorySeparatorChar + localFileName;
            }
            
            return result;
        }
    }
}
