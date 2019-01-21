using COSXML.Auth;
/**
* Copyright (c) 2018 Tencent Cloud. All rights reserved.
* 11/19/2018 4:37:05 PM
* bradyxiao
*/
namespace Demo.COS.Auth
{
    public sealed class AuthSamples
    {
        public static void testCAM(string secretId, string secretKey, long expire, string token)
        {
            DefaultSessionQCloudCredentialProvider defaultSessionCredentials = new DefaultSessionQCloudCredentialProvider(secretId, secretKey, expire, token);

        }
    }
}
