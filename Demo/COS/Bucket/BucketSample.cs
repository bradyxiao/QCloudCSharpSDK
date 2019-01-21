using System;
using System.Collections.Generic;
using COSXML.Model.Bucket;
using COSXML.Utils;
using COSXML.Log;
using COSXML.Common;
using COSXML.CosException;
using COSXML.Model;
using System.Threading;
/**
* Copyright (c) 2018 Tencent Cloud. All rights reserved.
* 11/12/2018 5:31:57 PM
* bradyxiao
*/
namespace Demo.COS.Bucket
{
    class BucketSample
    {

        public static void PutBucket(COSXML.CosXml cosXml, string bucket)
        {
            try
            {
                PutBucketRequest request = new PutBucketRequest(bucket);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
                //执行请求
                PutBucketResult result = cosXml.PutBucket(request);
                
                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                QLog.D("XIAO", clientEx.Message);
                Console.WriteLine("CosClientException: " + clientEx.Message);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                QLog.D("XIAO", serverEx.Message);
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }

        }

        public static void AsynPutBucket(COSXML.CosXml cosXml, string bucket)
        {
            PutBucketRequest request = new PutBucketRequest(bucket);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

            ///执行请求
            cosXml.PutBucket(request,
                delegate(CosResult cosResult)
                {
                    PutBucketResult result = cosResult as PutBucketResult;
                    Console.WriteLine(result.GetResultInfo());
                    Console.WriteLine(String.Format("currentThread id = {0}", Thread.CurrentThread.ManagedThreadId));
                },
                delegate(CosClientException clientEx, CosServerException serverEx)
                {
                    if (clientEx != null)
                    {
                        QLog.D("XIAO", clientEx.Message);
                        Console.WriteLine("CosClientException: " + clientEx.StackTrace);
                    }
                    if (serverEx != null)
                    {
                        QLog.D("XIAO", serverEx.Message);
                        Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                    }
                    Console.WriteLine(String.Format("currentThread id = {0}", Thread.CurrentThread.ManagedThreadId));
                });

        }

        public static void DeleteBucket(COSXML.CosXml cosXml, string bucket)
        {
            try
            {
                DeleteBucketRequest request = new DeleteBucketRequest(bucket);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
                //执行请求
                DeleteBucketResult result = cosXml.DeleteBucket(request);

                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                QLog.D("XIAO", clientEx.Message);
                Console.WriteLine("CosClientException: " + clientEx.Message);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                QLog.D("XIAO", serverEx.Message);
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }

        }

        public static void AsynDeleteBucket(COSXML.CosXml cosXml, string bucket)
        {
            DeleteBucketRequest request = new DeleteBucketRequest(bucket);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

            ///执行请求
            cosXml.DeleteBucket(request,
                delegate(CosResult cosResult)
                {
                    DeleteBucketResult result = cosResult as DeleteBucketResult;
                    Console.WriteLine(result.GetResultInfo());
                },
                delegate(CosClientException clientEx, CosServerException serverEx)
                {
                    if (clientEx != null)
                    {
                        QLog.D("XIAO", clientEx.Message);
                        Console.WriteLine("CosClientException: " + clientEx.StackTrace);
                    }
                    if (serverEx != null)
                    {
                        QLog.D("XIAO", serverEx.Message);
                        Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                    }
                });

        }

        public static void HeadBucket(COSXML.CosXml cosXml, string bucket)
        {
            try
            {
                HeadBucketRequest request = new HeadBucketRequest(bucket);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
                //执行请求
                HeadBucketResult result = cosXml.HeadBucket(request);
                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                QLog.D("XIAO", clientEx.Message);
                Console.WriteLine("CosClientException: " + clientEx.Message);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                QLog.D("XIAO", serverEx.Message);
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }

        }

        public static void AsynHeadBucket(COSXML.CosXml cosXml, string bucket)
        {
            HeadBucketRequest request = new HeadBucketRequest(bucket);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

            ///执行请求
            cosXml.HeadBucket(request,
                delegate(CosResult cosResult)
                {
                    HeadBucketResult result = cosResult as HeadBucketResult;
                    Console.WriteLine(result.GetResultInfo());
                },
                delegate(CosClientException clientEx, CosServerException serverEx)
                {
                    if (clientEx != null)
                    {
                        QLog.D("XIAO", clientEx.Message);
                        Console.WriteLine("CosClientException: " + clientEx.StackTrace);
                    }
                    if (serverEx != null)
                    {
                        QLog.D("XIAO", serverEx.Message);
                        Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                    }
                });

        }

        public static void GetBucket(COSXML.CosXml cosXml, string bucket)
        {

            GetBucketRequest request = new GetBucketRequest(bucket);

            //设置签名有效时长
            //request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

            request.SetPrefix("a/中文/d");

            List<string> headerKeys = new List<string>();
            headerKeys.Add("Host");


            List<string> queryParameters = new List<string>();
            queryParameters.Add("prefix");
            queryParameters.Add("max-keys");

            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600, headerKeys, queryParameters);


            //执行请求
            GetBucketResult result = cosXml.GetBucket(request);
            Console.WriteLine(result.GetResultInfo());

            //try
            //{
            //    GetBucketRequest request = new GetBucketRequest(bucket);
              
            //    //设置签名有效时长
            //    //request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

            //    request.SetPrefix("a/中文/d");
                
            //    List<string> headerKeys = new List<string>();
            //    headerKeys.Add("Host");
                

            //    List<string> queryParameters = new List<string>();
            //    queryParameters.Add("prefix");
            //    queryParameters.Add("max-keys");

            //    request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600, headerKeys, queryParameters);


            //    //执行请求
            //    GetBucketResult result = cosXml.GetBucket(request);
            //    Console.WriteLine(result.GetResultInfo());
            //}
            //catch (COSXML.CosException.CosClientException clientEx)
            //{
            //    QLog.D("XIAO", clientEx.Message);
            //    Console.WriteLine("CosClientException: " + clientEx.Message);
            //}
            //catch (COSXML.CosException.CosServerException serverEx)
            //{
            //    QLog.D("XIAO", serverEx.Message);
            //    Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            //}

        }

        public static void AsynGetBucket(COSXML.CosXml cosXml, string bucket)
        {
            GetBucketRequest request = new GetBucketRequest(bucket);
            request.SetPrefix("a/bed/d");
            List<string> queryParameters = new List<string>();
            queryParameters.Add("prefix");
            queryParameters.Add("max-keys");
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600, null, queryParameters);
            ////设置签名有效时长
            //request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);


            ///执行请求
            cosXml.GetBucket(request,
                delegate(CosResult cosResult)
                {
                    GetBucketResult result = cosResult as GetBucketResult;
                    Console.WriteLine(result.GetResultInfo());
                },
                delegate(CosClientException clientEx, CosServerException serverEx)
                {
                    if (clientEx != null)
                    {
                        QLog.D("XIAO", clientEx.Message);
                        Console.WriteLine("CosClientException: " + clientEx.Message);
                    }
                    if (serverEx != null)
                    {
                        QLog.D("XIAO", serverEx.Message);
                        Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                    }
                });

        }

        public static void GetBuckeLocation(COSXML.CosXml cosXml, string bucket)
        {
            try
            {
                GetBucketLocationRequest request = new GetBucketLocationRequest(bucket);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
                //执行请求
                GetBucketLocationResult result = cosXml.GetBucketLocation(request);
                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                QLog.D("XIAO", clientEx.Message);
                Console.WriteLine("CosClientException: " + clientEx.Message);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                QLog.D("XIAO", serverEx.Message);
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }

        }

        public static void AsynGetBuckeLocation(COSXML.CosXml cosXml, string bucket)
        {
            GetBucketLocationRequest request = new GetBucketLocationRequest(bucket);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

            ///执行请求
            cosXml.GetBucketLocation(request,
                delegate(CosResult cosResult)
                {
                    GetBucketLocationResult result = cosResult as GetBucketLocationResult;
                    Console.WriteLine(result.GetResultInfo());
                },
                delegate(CosClientException clientEx, CosServerException serverEx)
                {
                    if (clientEx != null)
                    {
                        QLog.D("XIAO", clientEx.Message);
                        Console.WriteLine("CosClientException: " + clientEx.Message);
                    }
                    if (serverEx != null)
                    {
                        QLog.D("XIAO", serverEx.Message);
                        Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                    }
                });
        }

        public static void PutBucketACL(COSXML.CosXml cosXml, string bucket)
        {
            try
            {
                PutBucketACLRequest request = new PutBucketACLRequest(bucket);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

                //添加acl
                request.SetCosACL(CosACL.PUBLIC_READ);

                COSXML.Model.Tag.GrantAccount readAccount = new COSXML.Model.Tag.GrantAccount();
                readAccount.AddGrantAccount("1131975903", "1131975903");
                request.SetXCosGrantRead(readAccount);

                COSXML.Model.Tag.GrantAccount writeAccount = new COSXML.Model.Tag.GrantAccount();
                writeAccount.AddGrantAccount("1131975903", "1131975903");
                request.SetXCosGrantWrite(writeAccount);

                COSXML.Model.Tag.GrantAccount fullAccount = new COSXML.Model.Tag.GrantAccount();
                fullAccount.AddGrantAccount("2832742109", "2832742109");
                request.SetXCosReadWrite(fullAccount);

                //执行请求
                PutBucketACLResult result = cosXml.PutBucketACL(request);
                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                QLog.D("XIAO", clientEx.Message);
                Console.WriteLine("CosClientException: " + clientEx.Message);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                QLog.D("XIAO", serverEx.Message);
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }
        }

        public static void AsynPutBucketACL(COSXML.CosXml cosXml, string bucket)
        {
            PutBucketACLRequest request = new PutBucketACLRequest(bucket);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

            //添加acl
            request.SetCosACL(CosACL.PRIVATE);

            COSXML.Model.Tag.GrantAccount readAccount = new COSXML.Model.Tag.GrantAccount();
            readAccount.AddGrantAccount("1131975903", "1131975903");
            request.SetXCosGrantRead(readAccount);

            COSXML.Model.Tag.GrantAccount writeAccount = new COSXML.Model.Tag.GrantAccount();
            writeAccount.AddGrantAccount("1131975903", "1131975903");
            request.SetXCosGrantWrite(writeAccount);

            COSXML.Model.Tag.GrantAccount fullAccount = new COSXML.Model.Tag.GrantAccount();
            fullAccount.AddGrantAccount("2832742109", "2832742109");
            request.SetXCosReadWrite(fullAccount);

            ///执行请求
            cosXml.PutBucketACL(request,
                delegate(CosResult cosResult)
                {
                    PutBucketACLResult result = cosResult as PutBucketACLResult;
                    Console.WriteLine(result.GetResultInfo());
                },
                delegate(CosClientException clientEx, CosServerException serverEx)
                {
                    if (clientEx != null)
                    {
                        QLog.D("XIAO", clientEx.Message);
                        Console.WriteLine("CosClientException: " + clientEx.Message);
                    }
                    if (serverEx != null)
                    {
                        QLog.D("XIAO", serverEx.Message);
                        Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                    }
                });
        }

        public static void GetBucketACL(COSXML.CosXml cosXml, string bucket)
        {
            try
            {
                GetBucketACLRequest request = new GetBucketACLRequest(bucket);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
                //执行请求
                GetBucketACLResult result = cosXml.GetBucketACL(request);
                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                QLog.D("XIAO", clientEx.Message);
                Console.WriteLine("CosClientException: " + clientEx.StackTrace);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                QLog.D("XIAO", serverEx.Message);
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }

        }
        
        public static void AsynGetBucketACL(COSXML.CosXml cosXml, string bucket)
        {
            GetBucketACLRequest request = new GetBucketACLRequest(bucket);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

            ///执行请求
            cosXml.GetBucketACL(request,
                delegate(CosResult cosResult)
                {
                    GetBucketACLResult result = cosResult as GetBucketACLResult;
                    Console.WriteLine(result.GetResultInfo());
                },
                delegate(CosClientException clientEx, CosServerException serverEx)
                {
                    if (clientEx != null)
                    {
                        QLog.D("XIAO", clientEx.Message);
                        Console.WriteLine("CosClientException: " + clientEx.Message);
                    }
                    if (serverEx != null)
                    {
                        QLog.D("XIAO", serverEx.Message);
                        Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                    }
                });
        }

        public static void PutBucketCORS(COSXML.CosXml cosXml, string bucket)
        {
            try
            {
                PutBucketCORSRequest request = new PutBucketCORSRequest(bucket);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

                //设置cors
                COSXML.Model.Tag.CORSConfiguration.CORSRule corsRule = new COSXML.Model.Tag.CORSConfiguration.CORSRule();

                corsRule.id = "corsconfigure1";
                corsRule.maxAgeSeconds = 6000;
                corsRule.allowedOrigin = "http://cloud.tencent.com";
                
                corsRule.allowedMethods = new List<string>();
                corsRule.allowedMethods.Add("PUT");
                corsRule.allowedMethods.Add("DELETE");
                corsRule.allowedMethods.Add("POST");
                
                corsRule.allowedHeaders = new List<string>();
                corsRule.allowedHeaders.Add("Host");
                corsRule.allowedHeaders.Add("Authorizaiton");
                corsRule.allowedHeaders.Add("User-Agent");

                corsRule.exposeHeaders = new List<string>();
                corsRule.exposeHeaders.Add("x-cos-meta-x1");
                corsRule.exposeHeaders.Add("x-cos-meta-x2");

                request.SetCORSRule(corsRule);

                //执行请求
                PutBucketCORSResult result = cosXml.PutBucketCORS(request);

                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                QLog.D("XIAO", clientEx.Message);
                Console.WriteLine("CosClientException: " + clientEx.Message);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                QLog.D("XIAO", serverEx.Message);
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }

        }

        public static void AsynPutBucketCORS(COSXML.CosXml cosXml, string bucket)
        {
            PutBucketCORSRequest request = new PutBucketCORSRequest(bucket);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
             
            //设置cors
            COSXML.Model.Tag.CORSConfiguration.CORSRule corsRule = new COSXML.Model.Tag.CORSConfiguration.CORSRule();

            corsRule.id = "corsconfigure1";
            corsRule.maxAgeSeconds = 6000;
            corsRule.allowedOrigin = "http://cloud.tencent.com";

            corsRule.allowedMethods = new List<string>();
            corsRule.allowedMethods.Add("PUT");
            corsRule.allowedMethods.Add("DELETE");
            corsRule.allowedMethods.Add("POST");

            corsRule.allowedHeaders = new List<string>();
            corsRule.allowedHeaders.Add("Host");
            corsRule.allowedHeaders.Add("Authorizaiton");
            corsRule.allowedHeaders.Add("User-Agent");

            corsRule.exposeHeaders = new List<string>();
            corsRule.exposeHeaders.Add("x-cos-meta-x1");
            corsRule.exposeHeaders.Add("x-cos-meta-x2");

            request.SetCORSRule(corsRule);

            ///执行请求
            cosXml.PutBucketCORS(request,
                delegate(CosResult cosResult)
                {
                    PutBucketCORSResult result = cosResult as PutBucketCORSResult;
                    Console.WriteLine(result.GetResultInfo());
                },
                delegate(CosClientException clientEx, CosServerException serverEx)
                {
                    if (clientEx != null)
                    {
                        QLog.D("XIAO", clientEx.Message);
                        Console.WriteLine("CosClientException: " + clientEx.Message);
                    }
                    if (serverEx != null)
                    {
                        QLog.D("XIAO", serverEx.Message);
                        Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                    }
                });
        }

        public static void GetBucketCORS(COSXML.CosXml cosXml, string bucket)
        {
            try
            {
                GetBucketCORSRequest request = new GetBucketCORSRequest(bucket);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
                //执行请求
                GetBucketCORSResult result = cosXml.GetBucketCORS(request);
        
                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                QLog.D("XIAO", clientEx.Message);
                Console.WriteLine("CosClientException: " + clientEx.Message);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                QLog.D("XIAO", serverEx.Message);
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }

        }

        public static void AsynGetBucketCORS(COSXML.CosXml cosXml, string bucket)
        {
            GetBucketCORSRequest request = new GetBucketCORSRequest(bucket);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

            ///执行请求
            cosXml.GetBucketCORS(request,
                delegate(CosResult cosResult)
                {
                    GetBucketCORSResult result = cosResult as GetBucketCORSResult;
                    Console.WriteLine(result.GetResultInfo());
                },
                delegate(CosClientException clientEx, CosServerException serverEx)
                {
                    if (clientEx != null)
                    {
                        QLog.D("XIAO", clientEx.Message);
                        Console.WriteLine("CosClientException: " + clientEx.Message);
                    }
                    if (serverEx != null)
                    {
                        QLog.D("XIAO", serverEx.Message);
                        Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                    }
                });
        }

        public static void DeleteBucketCORS(COSXML.CosXml cosXml, string bucket)
        {
            try
            {
                DeleteBucketCORSRequest request = new DeleteBucketCORSRequest(bucket);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
                //执行请求
                DeleteBucketCORSResult result = cosXml.DeleteBucketCORS(request);

                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                QLog.D("XIAO", clientEx.Message);
                Console.WriteLine("CosClientException: " + clientEx.Message);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                QLog.D("XIAO", serverEx.Message);
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }

        }

        public static void AsynDeleteBucketCORS(COSXML.CosXml cosXml, string bucket)
        {
            DeleteBucketCORSRequest request = new DeleteBucketCORSRequest(bucket);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

            ///执行请求
            cosXml.DeleteBucketCORS(request,
                delegate(CosResult cosResult)
                {
                    DeleteBucketCORSResult result = cosResult as DeleteBucketCORSResult;
                    Console.WriteLine(result.GetResultInfo());
                },
                delegate(CosClientException clientEx, CosServerException serverEx)
                {
                    if (clientEx != null)
                    {
                        QLog.D("XIAO", clientEx.Message);
                        Console.WriteLine("CosClientException: " + clientEx.Message);
                    }
                    if (serverEx != null)
                    {
                        QLog.D("XIAO", serverEx.Message);
                        Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                    }
                });
        }

        public static void PutBucketLifeCycle(COSXML.CosXml cosXml, string bucket)
        {
            try
            {
                PutBucketLifecycleRequest request = new PutBucketLifecycleRequest(bucket);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

                //设置 lifecycle
                COSXML.Model.Tag.LifecycleConfiguration.Rule rule = new COSXML.Model.Tag.LifecycleConfiguration.Rule();
                rule.id = "lfiecycleConfigure2";
                rule.status = "Enabled"; //Enabled，Disabled
                
                rule.filter = new COSXML.Model.Tag.LifecycleConfiguration.Filter();
                rule.filter.prefix = "2/";

                rule.abortIncompleteMultiUpload = new COSXML.Model.Tag.LifecycleConfiguration.AbortIncompleteMultiUpload();
                rule.abortIncompleteMultiUpload.daysAfterInitiation = 2;
                     
                request.SetRule(rule);

                //执行请求
                PutBucketLifecycleResult result = cosXml.PutBucketLifecycle(request);

                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                QLog.D("XIAO", clientEx.Message);
                Console.WriteLine("CosClientException: " + clientEx.Message);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                QLog.D("XIAO", serverEx.Message);
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }

        }

        public static void AsynPutBucketLifeCycle(COSXML.CosXml cosXml, string bucket)
        {
            PutBucketLifecycleRequest request = new PutBucketLifecycleRequest(bucket);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

            //设置 lifecycle
            COSXML.Model.Tag.LifecycleConfiguration.Rule rule = new COSXML.Model.Tag.LifecycleConfiguration.Rule();
            rule.id = "lfiecycleConfig3";
            rule.status = "Enabled"; //Enabled，Disabled

            rule.filter = new COSXML.Model.Tag.LifecycleConfiguration.Filter();
            rule.filter.prefix = "3";

            rule.abortIncompleteMultiUpload = new COSXML.Model.Tag.LifecycleConfiguration.AbortIncompleteMultiUpload();
            rule.abortIncompleteMultiUpload.daysAfterInitiation = 2;

            request.SetRule(rule);

            ///执行请求
            cosXml.PutBucketLifecycle(request,
                delegate(CosResult cosResult)
                {
                    PutBucketLifecycleResult result = cosResult as PutBucketLifecycleResult;
                    Console.WriteLine(result.GetResultInfo());
                },
                delegate(CosClientException clientEx, CosServerException serverEx)
                {
                    if (clientEx != null)
                    {
                        QLog.D("XIAO", clientEx.Message);
                        Console.WriteLine("CosClientException: " + clientEx.Message);
                    }
                    if (serverEx != null)
                    {
                        QLog.D("XIAO", serverEx.Message);
                        Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                    }
                });
        }


        public static void GetBucketLifeCycle(COSXML.CosXml cosXml, string bucket)
        {
            try
            {
                GetBucketLifecycleRequest request = new GetBucketLifecycleRequest(bucket);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
                //执行请求
                GetBucketLifecycleResult result = cosXml.GetBucketLifecycle(request);

                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                QLog.D("XIAO", clientEx.Message);
                Console.WriteLine("CosClientException: " + clientEx.Message);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                QLog.D("XIAO", serverEx.Message);
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }

        }

        public static void AsynGetBucketLifeCycle(COSXML.CosXml cosXml, string bucket)
        {
            GetBucketLifecycleRequest request = new GetBucketLifecycleRequest(bucket);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

            ///执行请求
            cosXml.GetBucketLifecycle(request,
                delegate(CosResult cosResult)
                {
                    GetBucketLifecycleResult result = cosResult as GetBucketLifecycleResult;
                    Console.WriteLine(result.GetResultInfo());
                },
                delegate(CosClientException clientEx, CosServerException serverEx)
                {
                    if (clientEx != null)
                    {
                        QLog.D("XIAO", clientEx.Message);
                        Console.WriteLine("CosClientException: " + clientEx.Message);
                    }
                    if (serverEx != null)
                    {
                        QLog.D("XIAO", serverEx.Message);
                        Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                    }
                });
        }

        public static void DeleteBucketLifeCycle(COSXML.CosXml cosXml, string bucket)
        {
            try
            {
                DeleteBucketLifecycleRequest request = new DeleteBucketLifecycleRequest(bucket);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
                //执行请求
                DeleteBucketLifecycleResult result = cosXml.DeleteBucketLifecycle(request);

                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                QLog.D("XIAO", clientEx.Message);
                Console.WriteLine("CosClientException: " + clientEx.Message);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                QLog.D("XIAO", serverEx.Message);
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }

        }

        public static void AsynDeleteBucketLifeCycle(COSXML.CosXml cosXml, string bucket)
        { 
            DeleteBucketLifecycleRequest request = new DeleteBucketLifecycleRequest(bucket);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

            ///执行请求
            cosXml.DeleteBucketLifecycle(request,
                delegate(CosResult cosResult)
                {
                    DeleteBucketLifecycleResult result = cosResult as DeleteBucketLifecycleResult;
                    Console.WriteLine(result.GetResultInfo());
                },
                delegate(CosClientException clientEx, CosServerException serverEx)
                {
                    if (clientEx != null)
                    {
                        QLog.D("XIAO", clientEx.Message);
                        Console.WriteLine("CosClientException: " + clientEx.Message);
                    }
                    if (serverEx != null)
                    {
                        QLog.D("XIAO", serverEx.Message);
                        Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                    }
                });
        }

        public static void PutBucketReplication(COSXML.CosXml cosXml, string bucket)
        {
            try
            {
                PutBucketReplicationRequest request = new PutBucketReplicationRequest(bucket);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

                //设置replication
                PutBucketReplicationRequest.RuleStruct ruleStruct = new PutBucketReplicationRequest.RuleStruct();
                ruleStruct.appid = "";
                ruleStruct.bucket = "";
                ruleStruct.region = "";
                ruleStruct.isEnable = true;
                ruleStruct.storageClass = "";
                ruleStruct.id = "";
                ruleStruct.prefix = "";
                List<PutBucketReplicationRequest.RuleStruct> ruleStructs = new List<PutBucketReplicationRequest.RuleStruct>();
                ruleStructs.Add(ruleStruct);
                request.SetReplicationConfiguration("2832742109", "2832742109", ruleStructs);

                //执行请求
                PutBucketReplicationResult result = cosXml.PutBucketReplication(request);

                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                QLog.D("XIAO", clientEx.Message);
                Console.WriteLine("CosClientException: " + clientEx.Message);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                QLog.D("XIAO", serverEx.Message);
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }

        }

        public static void AsynPutBucketReplication(COSXML.CosXml cosXml, string bucket)
        {
            PutBucketReplicationRequest request = new PutBucketReplicationRequest(bucket);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
            //设置replication
            PutBucketReplicationRequest.RuleStruct ruleStruct = new PutBucketReplicationRequest.RuleStruct();
            ruleStruct.appid = "";
            ruleStruct.bucket = "";
            ruleStruct.region = "";
            ruleStruct.isEnable = true;
            ruleStruct.storageClass = "";
            ruleStruct.id = "";
            ruleStruct.prefix = "";
            List<PutBucketReplicationRequest.RuleStruct> ruleStructs = new List<PutBucketReplicationRequest.RuleStruct>();
            ruleStructs.Add(ruleStruct);
            request.SetReplicationConfiguration("2832742109", "2832742109", ruleStructs);
            ///执行请求
            cosXml.PutBucketReplication(request,
                delegate(CosResult cosResult)
                {
                    PutBucketReplicationResult result = cosResult as PutBucketReplicationResult;
                    Console.WriteLine(result.GetResultInfo());
                },
                delegate(CosClientException clientEx, CosServerException serverEx)
                {
                    if (clientEx != null)
                    {
                        QLog.D("XIAO", clientEx.Message);
                        Console.WriteLine("CosClientException: " + clientEx.Message);
                    }
                    if (serverEx != null)
                    {
                        QLog.D("XIAO", serverEx.Message);
                        Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                    }
                });
        }

        public static void GetBucketReplication(COSXML.CosXml cosXml, string bucket)
        {
            try
            {
                GetBucketReplicationRequest request = new GetBucketReplicationRequest(bucket);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
                //执行请求
                GetBucketReplicationResult result = cosXml.GetBucketReplication(request);

                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                QLog.D("XIAO", clientEx.Message);
                Console.WriteLine("CosClientException: " + clientEx.Message);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                QLog.D("XIAO", serverEx.Message);
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }

        }

        public static void AsynGetBucketReplication(COSXML.CosXml cosXml, string bucket)
        {
            GetBucketReplicationRequest request = new GetBucketReplicationRequest(bucket);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
           
           
            ///执行请求
            cosXml.GetBucketReplication(request,
                delegate(CosResult cosResult)
                {
                    GetBucketReplicationResult result = cosResult as GetBucketReplicationResult;
                    Console.WriteLine(result.GetResultInfo());
                },
                delegate(CosClientException clientEx, CosServerException serverEx)
                {
                    if (clientEx != null)
                    {
                        QLog.D("XIAO", clientEx.Message);
                        Console.WriteLine("CosClientException: " + clientEx.Message);
                    }
                    if (serverEx != null)
                    {
                        QLog.D("XIAO", serverEx.Message);
                        Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                    }
                });
        }


        public static void DeleteBucketReplication(COSXML.CosXml cosXml, string bucket)
        {
            try
            {
                DeleteBucketReplicationRequest request = new DeleteBucketReplicationRequest(bucket);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
                //执行请求
                DeleteBucketReplicationResult result = cosXml.DeleteBucketReplication(request);

                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                QLog.D("XIAO", clientEx.Message);
                Console.WriteLine("CosClientException: " + clientEx.Message);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                QLog.D("XIAO", serverEx.Message);
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }

        }

        public static void AsynDeleteBucketReplication(COSXML.CosXml cosXml, string bucket)
        {
            DeleteBucketReplicationRequest request = new DeleteBucketReplicationRequest(bucket);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);


            ///执行请求
            cosXml.DeleteBucketReplication(request,
                delegate(CosResult cosResult)
                {
                    DeleteBucketReplicationResult result = cosResult as DeleteBucketReplicationResult;
                    Console.WriteLine(result.GetResultInfo());
                },
                delegate(CosClientException clientEx, CosServerException serverEx)
                {
                    if (clientEx != null)
                    {
                        QLog.D("XIAO", clientEx.Message);
                        Console.WriteLine("CosClientException: " + clientEx.Message);
                    }
                    if (serverEx != null)
                    {
                        QLog.D("XIAO", serverEx.Message);
                        Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                    }
                });
        }

        public static void PutBucketVersioning(COSXML.CosXml cosXml, string bucket)
        {
            try
            {
                PutBucketVersioningRequest request = new PutBucketVersioningRequest(bucket);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

                //开启版本控制
                request.IsEnableVersionConfig(true);

                //执行请求
                PutBucketVersioningResult result = cosXml.PutBucketVersioning(request);

                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                QLog.D("XIAO", clientEx.Message);
                Console.WriteLine("CosClientException: " + clientEx.Message);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                QLog.D("XIAO", serverEx.Message);
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }

        }

        public static void AsynPutBucketVersioning(COSXML.CosXml cosXml, string bucket)
        {
            PutBucketVersioningRequest request = new PutBucketVersioningRequest(bucket);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

            //开启版本控制
            request.IsEnableVersionConfig(true);


            ///执行请求
            cosXml.PutBucketVersioning(request,
                delegate(CosResult cosResult)
                {
                    PutBucketVersioningResult result = cosResult as PutBucketVersioningResult;
                    Console.WriteLine(result.GetResultInfo());
                },
                delegate(CosClientException clientEx, CosServerException serverEx)
                {
                    if (clientEx != null)
                    {
                        QLog.D("XIAO", clientEx.Message);
                        Console.WriteLine("CosClientException: " + clientEx.Message);
                    }
                    if (serverEx != null)
                    {
                        QLog.D("XIAO", serverEx.Message);
                        Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                    }
                });
        }

        public static void GetBucketVersioning(COSXML.CosXml cosXml, string bucket)
        {
            try
            {
                GetBucketVersioningRequest request = new GetBucketVersioningRequest(bucket);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
                //执行请求
                GetBucketVersioningResult result = cosXml.GetBucketVersioning(request);

                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                QLog.D("XIAO", clientEx.Message);
                Console.WriteLine("CosClientException: " + clientEx.Message);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                QLog.D("XIAO", serverEx.Message);
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }

        }

        public static void AsynGetBucketVersioning(COSXML.CosXml cosXml, string bucket)
        {
            GetBucketVersioningRequest request = new GetBucketVersioningRequest(bucket);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

            //执行请求
            cosXml.GetBucketVersioning(request,
                delegate(CosResult cosResult)
                {
                    GetBucketVersioningResult result = cosResult as GetBucketVersioningResult;
                    Console.WriteLine(result.GetResultInfo());
                },
                delegate(CosClientException clientEx, CosServerException serverEx)
                {
                    if (clientEx != null)
                    {
                        QLog.D("XIAO", clientEx.Message);
                        Console.WriteLine("CosClientException: " + clientEx.Message);
                    }
                    if (serverEx != null)
                    {
                        QLog.D("XIAO", serverEx.Message);
                        Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                    }
                });
        }

        public static void ListBucketVersions(COSXML.CosXml cosXml, string bucket)
        {
            try
            {
                ListBucketVersionsRequest request = new ListBucketVersionsRequest(bucket);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
                //执行请求
                ListBucketVersionsResult result = cosXml.ListBucketVersions(request);

                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                QLog.D("XIAO", clientEx.Message);
                Console.WriteLine("CosClientException: " + clientEx.Message);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                QLog.D("XIAO", serverEx.Message);
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }

        }

        public static void AsynListBucketVersions(COSXML.CosXml cosXml, string bucket)
        {
            ListBucketVersionsRequest request = new ListBucketVersionsRequest(bucket);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

            //执行请求
            cosXml.ListBucketVersions(request,
                delegate(CosResult cosResult)
                {
                    ListBucketVersionsResult result = cosResult as ListBucketVersionsResult;
                    Console.WriteLine(result.GetResultInfo());
                },
                delegate(CosClientException clientEx, CosServerException serverEx)
                {
                    if (clientEx != null)
                    {
                        QLog.D("XIAO", clientEx.Message);
                        Console.WriteLine("CosClientException: " + clientEx.Message);
                    }
                    if (serverEx != null)
                    {
                        QLog.D("XIAO", serverEx.Message);
                        Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                    }
                });
        }

        public static void ListMultiUploads(COSXML.CosXml cosXml, string bucket)
        {
            try
            {
                ListMultiUploadsRequest request = new ListMultiUploadsRequest(bucket);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
                //执行请求
                ListMultiUploadsResult result = cosXml.ListMultiUploads(request);

                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                QLog.D("XIAO", clientEx.Message);
                Console.WriteLine("CosClientException: " + clientEx.Message);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                QLog.D("XIAO", serverEx.Message);
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }

        }

        public static void AsynListMultiUploads(COSXML.CosXml cosXml, string bucket)
        {
            ListMultiUploadsRequest request = new ListMultiUploadsRequest(bucket);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

            //执行请求
            cosXml.ListMultiUploads(request,
                delegate(CosResult cosResult)
                {
                    ListMultiUploadsResult result = cosResult as ListMultiUploadsResult;
                    Console.WriteLine(result.GetResultInfo());
                },
                delegate(CosClientException clientEx, CosServerException serverEx)
                {
                    if (clientEx != null)
                    {
                        QLog.D("XIAO", clientEx.Message);
                        Console.WriteLine("CosClientException: " + clientEx.Message);
                    }
                    if (serverEx != null)
                    {
                        QLog.D("XIAO", serverEx.Message);
                        Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                    }
                });
        }

        

    }
}
