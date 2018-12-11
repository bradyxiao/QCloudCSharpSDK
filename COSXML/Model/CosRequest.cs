using System;
using System.Collections.Generic;

using System.Text;
using COSXML.Common;
using COSXML.Network;
using COSXML.Log;
using COSXML.Auth;
/**
* Copyright (c) 2018 Tencent Cloud. All rights reserved.
* 11/2/2018 1:05:09 PM
* bradyxiao
*/
namespace COSXML.Model
{
    /**
     * cos request base class, all cos operator must be extended this.
     * 
     * request method | request path | query parameters
     * request headers
     * request body
     * 
     * special:
     * cos sign;
     */
    public abstract class CosRequest
    {
        private static string TAG = typeof(CosRequest).FullName;

        protected bool? isHttps = null;

        protected string method = CosRequestMethod.GET;

        protected string path;

        protected Dictionary<string, string> queryParameters = new Dictionary<string,string>();

        protected Dictionary<string, string> headers = new Dictionary<string,string>();

        protected string appid;

        protected CosXmlSignSourceProvider cosXmlSignSourceProvider = new CosXmlSignSourceProvider();

        protected bool needMD5 = false;

        /// <summary>
        /// http or https for cos request.
        /// </summary>
        public bool? IsHttps 
        {
            get { return isHttps; }
            set { isHttps = value; }
        }

        public string Method
        {
            get { return method; }
            private set { }
        }

        public string RequestPath 
        {
            get { return path; }
            private set { }
        }

        public virtual Dictionary<string, string> GetRequestParamters()
        {
            return queryParameters;
        }

        public virtual Dictionary<string, string> GetRequestHeaders()
        {
            return headers;
        }

        /// <summary>
        /// add query parameter for cos request, and cover the current value if it exists with the key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetQueryParameter(string key, string value)
        {
            try
            {
                queryParameters.Add(key, value);
            }
            catch(ArgumentNullException)
            {
                QLog.D(TAG, "SetQueryParameter: key ==null");
            }
            catch(ArgumentException)
            {
                queryParameters[key] = value; // cover the current value
            }
        }

        /// <summary>
        /// add header for cos request, and cover the current value, if it exists with the key.
        /// </summary>
        /// <param name="key"> header: key </param>
        /// <param name="value"> header: value</param>
        public void SetRequestHeader(string key, string value)
        {
            try
            {
                headers.Add(key, value);
            }
            catch (ArgumentNullException)
            {
                QLog.D(TAG, "SetRequestHeader: key ==null");
            }
            catch (ArgumentException)
            {
                headers[key] = value; // cover the current value
            }
        }

        /// <summary>
        /// set appid for cos.
        /// </summary>
        /// <param name="appid"> cos appid </param>
        public string APPID
        {
            get { return this.appid; }
            set { this.appid = value; }
        }

        public bool IsNeedMD5
        {
            get { return needMD5; }
            set { needMD5 = value; }
        }

        /// <summary>
        /// return the host for cos request
        /// </summary>
        /// <returns>host(string)</returns>
        public abstract string GetHost();

        /// <summary>
        /// return the body for cos request. such as upload file.
        /// </summary>
        /// <returns> <see cref="COSXML.Network.RequestBody"/></returns>
        public abstract RequestBody GetRequestBody();

        /// <summary>    
        ///   check parameter for cos.
        /// </summary>
        /// <exception cref="COSXML.CosException.CosClientException"></exception>
        public abstract void CheckParameters();

        public virtual void SetSign(long signStartTimeSecond, long durationSecond)
        {
            if (cosXmlSignSourceProvider == null) cosXmlSignSourceProvider = new CosXmlSignSourceProvider();
            cosXmlSignSourceProvider.SetSignTime(signStartTimeSecond, durationSecond);
        }

        public virtual void SetSign(long signStartTimeSecond, long durationSecond, List<string> headerKeys, List<string> queryParameterKeys)
        {
            if (cosXmlSignSourceProvider == null) cosXmlSignSourceProvider = new CosXmlSignSourceProvider();
            cosXmlSignSourceProvider.SetSignTime(signStartTimeSecond, durationSecond);
            cosXmlSignSourceProvider.AddHeaderKeys(headerKeys);
            cosXmlSignSourceProvider.AddParameterKeys(queryParameterKeys);
        }

        public virtual void SetSign(string sign)
        {
            SetRequestHeader(CosRequestHeaderKey.AUTHORIZAIION, sign);
        }

        public virtual CosXmlSignSourceProvider GetSignSourceProvider()
        {
            return cosXmlSignSourceProvider;
        }
    }
}
