using System;
using System.Collections.Generic;

using System.Text;
using COSXML.Common;
using System.IO;
using COSXML.Utils;
using COSXML.CosException;
using COSXML.Auth;
using COSXML.Log;
using COSXML.Network;

namespace COSXML.Model.Object
{
    public sealed class PostObjectRequest : ObjectRequest
    {
        private static string TAG = typeof(PostObjectRequest).FullName;

        private FormStruct formStruct;

        private PostObjectRequest(string bucket, string key) 
            : base(bucket, "/") 
        {
            this.method = CosRequestMethod.POST;
            formStruct = new FormStruct();
            formStruct.key = key;
            this.headers.Add(CosRequestHeaderKey.CONTENT_TYPE, "multipart/form-data; boundary=" + MultipartRequestBody.BOUNDARY);
        }

        public PostObjectRequest(string bucket, string key, string srcPath)
            :this(bucket, key, srcPath, -1L, -1L)
        {
        }

        public PostObjectRequest(string bucket, string key, string srcPath, long fileOffset, long sendContentLength)
            : this(bucket, key)
        {
            formStruct.srcPath = srcPath;
            formStruct.fileOffset = fileOffset < 0 ? 0 : fileOffset;
            formStruct.contentLength = sendContentLength < 0L ? -1L : sendContentLength;
        }

        public PostObjectRequest(string bucket, string key, byte[] data)
            :this(bucket, key)
        {
            formStruct.data = data;
        }

        public void SetCosProgressCallback(COSXML.Callback.OnProgressCallback progressCallback)
        {
            formStruct.progressCallback = progressCallback;
        }

        public void SetCosACL(CosACL cosACL)
        {
            formStruct.acl = EnumUtils.GetValue(cosACL);
        }

        public void SetCacheControl(string cacheControl)
        {
            SetHeader("Cache-Control", cacheControl);
        }

        public void SetContentType(string contentType)
        {
            SetHeader("Content-Type", contentType);
        }

        public void SetContentDisposition(string contentDisposition)
        {
            SetHeader("Content-Disposition", contentDisposition);
        }

        public void SetContentEncoding(string contentEncoding)
        {
            SetHeader("Content-Encoding", contentEncoding);
        }

        public void SetExpires(string expires)
        {
            SetHeader("Expires", expires);
        }

        public void SetHeader(string key, string value)
        {
            try
            {
                formStruct.headers.Add(key, value);
            }
            catch (ArgumentNullException)
            {
                QLog.D(TAG, "SetHeader: key ==null");
            }
            catch (ArgumentException)
            {
                formStruct.headers[key] = value;
            }
        }

        public void SetCustomerHeader(string key, string value)
        {
            try
            {
                formStruct.customHeaders.Add(key, value);
            }
            catch (ArgumentNullException)
            {
                QLog.D(TAG, "SetHeader: key ==null");
            }
            catch (ArgumentException)
            {
                formStruct.customHeaders[key] = value;
            }
        }

        public void SetCosStorageClass(string cosStorageClass)
        {
            formStruct.xCosStorageClass = cosStorageClass;
        }

        /// <summary>
        /// the host you want to redirect
        /// </summary>
        /// <param name="redirectHost"></param>
        public void SetSuccessActionRedirect(string redirectHost)
        {
            formStruct.successActionRedirect = redirectHost;
        }

        /// <summary>
        /// successHttpCode can be 200, 201, 204, default value 204
        /// </summary>
        /// <param name="successHttpCode"></param>
        public void SetSuccessActionStatus(int successHttpCode)
        {
            formStruct.successActionStatus = successHttpCode.ToString();
        }

        public void SetPolicy(Policy policy)
        {
            formStruct.policy = policy;
        }

        public override void SetSign(string sign)
        {
            formStruct.sign = sign;
        }

        public override void CheckParameters()
        {
            base.CheckParameters();
            formStruct.CheckParameter();
        }

        public override CosXmlSignSourceProvider GetSignSourceProvider()
        {
            if (this.cosXmlSignSourceProvider != null)
            {
                this.cosXmlSignSourceProvider.onGetSign = delegate(Request request, string sign)
                {
                    //添加参数 sign
                    ((MultipartRequestBody)request.Body).AddParameter("Signature", sign);
                };
            }
            return base.GetSignSourceProvider();
        }

        public override RequestBody GetRequestBody()
        {
            MultipartRequestBody requestBody = new MultipartRequestBody();
            requestBody.AddParamters(formStruct.GetFormParameters());
            if (formStruct.data != null)
            {
                requestBody.AddData(formStruct.data, "file", "tmp");
            }
            else if (formStruct.srcPath != null)
            {
                FileInfo fileInfo = new FileInfo(this.formStruct.srcPath);
                string fileName = fileInfo.Name;
                if (formStruct.contentLength == -1L || formStruct.contentLength + formStruct.fileOffset > fileInfo.Length)
                {
                    formStruct.contentLength = fileInfo.Length - formStruct.fileOffset;
                }
                requestBody.AddData(formStruct.srcPath, formStruct.fileOffset, formStruct.contentLength, "file", fileName);
            }
            requestBody.ProgressCallback = formStruct.progressCallback;
            return requestBody;
        }

        private class FormStruct
        {
            public string acl;
            public Dictionary<string, string> headers;
            public string key;
            public string successActionRedirect;
            public string successActionStatus;
            public Dictionary<string, string> customHeaders;
            public string xCosStorageClass;
            public string sign;
            public Policy policy;
            public string srcPath;
            public long fileOffset = 0L;
            public long contentLength = -1L;
            public byte[] data;
            public COSXML.Callback.OnProgressCallback progressCallback;
            

            public FormStruct()
            {
                headers = new Dictionary<string, string>();
                customHeaders = new Dictionary<string, string>();
            }

            public Dictionary<string, string> GetFormParameters() 
            {
                Dictionary<string, string> formParameters = new Dictionary<string, string>();
                if(acl != null){
                    formParameters.Add("Acl", acl);
                }
                foreach(KeyValuePair<string, string> pair in headers){
                    formParameters.Add(pair.Key, pair.Value);
                }
                formParameters.Add("key", key);
                if(successActionRedirect != null){
                    formParameters.Add("success_action_redirect", successActionRedirect);
                }
                if(successActionStatus != null){
                    formParameters.Add("success_action_status", successActionStatus);
                }
                foreach(KeyValuePair<string, string> pair in customHeaders){
                    formParameters.Add(pair.Key, pair.Value);
                }
                if(xCosStorageClass != null){
                    formParameters.Add("x-cos-storage-class", xCosStorageClass);
                }
                if (sign != null)
                {
                    formParameters.Add("Signature", sign);
                }
                if(policy != null){
                    formParameters.Add("policy", DigestUtils.GetBase64(policy.Content(), Encoding.UTF8));
                }
                return formParameters;
            }

            public void CheckParameter()
            {
                if (key == null) throw new CosClientException((int)CosClientError.INVALID_ARGUMENT, "FormStruct.key = null");
                if (srcPath == null && data == null)
                {
                    throw new CosClientException((int)CosClientError.INVALID_ARGUMENT, "data source = null");
                }
                if (srcPath != null)
                {
                    if (!File.Exists(srcPath)) throw new CosClientException((int)CosClientError.INVALID_ARGUMENT, "srcPath not exist");
                }
            }
        }

        public class Policy
        {
            private string expiration;
            private StringBuilder conditions = new StringBuilder();

            public void SetExpiration(long endTimeMills)
            {
                this.expiration = TimeUtils.GetFormatTime("yyyy-MM-dd'T'HH:mm:ss.SSS'Z'", endTimeMills, TimeUnit.MILLISECONDS);
            }

            public void SetExpiration(string formatEndTime)
            {
                this.expiration = formatEndTime;
            }

            public void AddConditions(string key, string value, bool isPrefixMatch) 
            {
                if(isPrefixMatch){
                    conditions.Append('[')
                        .Append("starts-with")
                        .Append(',')
                        .Append(key)
                        .Append(',')
                        .Append(value)
                        .Append(']');
                }else {
                    conditions.Append('{')
                        .Append(key)
                        .Append(':')
                        .Append(value)
                        .Append('}');
                }
            }

            public void AddContentConditions(int start, int end)
            {
                conditions.Append('[')
                    .Append("content-length-range")
                    .Append(',')
                    .Append(start)
                    .Append(',')
                    .Append(end)
                    .Append(']');
            }

            public string Content()
            {
                StringBuilder content = new StringBuilder();
                content.Append('{');
                if (expiration != null)
                {
                    content.Append(String.Format("expiration:{0}", expiration));
                }
                content.Append(String.Format("conditions:{0}", conditions));
                content.Append('}');
                return content.ToString();
            }
        }
    }
}
