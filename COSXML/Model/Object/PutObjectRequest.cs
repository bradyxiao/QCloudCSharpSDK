using System;
using System.Collections.Generic;

using System.Text;
using COSXML.Common;
using System.IO;
using COSXML.Model.Tag;
using COSXML.Utils;
using COSXML.Log;
using COSXML.CosException;
using System.Threading;
using COSXML.Network;

namespace COSXML.Model.Object
{
    public sealed class PutObjectRequest : ObjectRequest
    {
        private static string TAG = typeof(PutObjectRequest).FullName;

        private string srcPath;
        private long fileOffset = 0L;

        private byte[] data;

        private long contentLength = -1L;
        private COSXML.Callback.OnProgressCallback progressCallback;

        public PutObjectRequest(string bucket, string key, string srcPath)
            :this(bucket, key, srcPath, -1L, -1L)
        {

        }

        public PutObjectRequest(string bucket, string key, string srcPath, long fileOffset, long needSendLength)
            :base(bucket, key)
        {
            this.method = CosRequestMethod.PUT;
            this.srcPath = srcPath;
            this.fileOffset = fileOffset < 0 ? 0 : fileOffset;
            this.contentLength = needSendLength < 0 ? -1L : needSendLength;
        }

        public PutObjectRequest(string bucket, string key, byte[] data) : base(bucket, key)
        {
            this.method = CosRequestMethod.PUT;
            this.data = data;
        }

        public void SetCosProgressCallback(COSXML.Callback.OnProgressCallback progressCallback)
        {
            this.progressCallback = progressCallback;
        }

        public override void CheckParameters()
        {
            base.CheckParameters();
            if(srcPath == null && data == null) throw new CosClientException((int)(CosClientError.INVALID_ARGUMENT), "data source = null");
            if(srcPath != null)
            {
                if(!File.Exists(srcPath)) throw  new CosClientException((int)(CosClientError.INVALID_ARGUMENT), "file not exist");
            }
        }

        public override Network.RequestBody GetRequestBody()
        {
            RequestBody body = null;
            if (srcPath != null)
            {
                FileInfo fileInfo = new FileInfo(srcPath);
                if (contentLength == -1 || contentLength + fileOffset > fileInfo.Length)
                {
                    contentLength = fileInfo.Length - fileOffset;
                }
                body = new FileRequestBody(srcPath, fileOffset, contentLength);
                body.ProgressCallback = progressCallback;
            }
            else if (data != null)
            {
                body = new ByteRequestBody(data);
                body.ProgressCallback = progressCallback;
            }
            return body;
        }

        public void SetCosACL(string cosACL)
        {
            if (cosACL != null)
            {
                SetRequestHeader(CosRequestHeaderKey.X_COS_ACL, cosACL);
            }
        }

        public void SetCosACL(CosACL cosACL)
        {
            SetRequestHeader(CosRequestHeaderKey.X_COS_ACL, EnumUtils.GetValue(cosACL));
        }

        public void setXCosGrantRead(GrantAccount grantAccount)
        {
            if (grantAccount != null)
            {
                SetRequestHeader(CosRequestHeaderKey.X_COS_GRANT_READ, grantAccount.GetGrantAccounts());
            }
        }

        public void setXCosGrantWrite(GrantAccount grantAccount)
        {
            if (grantAccount != null)
            {
                SetRequestHeader(CosRequestHeaderKey.X_COS_GRANT_WRITE, grantAccount.GetGrantAccounts());
            }
        }

        public void setXCosReadWrite(GrantAccount grantAccount)
        {
            if (grantAccount != null)
            {
                SetRequestHeader(CosRequestHeaderKey.X_COS_GRANT_FULL_CONTROL, grantAccount.GetGrantAccounts());
            }
        }

    }
}
