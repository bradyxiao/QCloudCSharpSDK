using System;
using System.Collections.Generic;
using System.Text;
using COSXML.Model.Service;
using COSXML;
using COSXML.Utils;
using COSXML.Log;

namespace Demo.COS.GetServiceSample
{
    class GetServiceAPI
    {
        public static void GetService(CosXml cosXml)
        {
            GetServiceRequest request = new GetServiceRequest();
            QLog.D("INFO", String.Format("current thread name: {0}-{1}", System.Threading.Thread.CurrentThread.Name, System.Threading.Thread.CurrentThread.ManagedThreadId));
            //设置签名有效时长
            //request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
            //执行请求
            GetServiceResult result = cosXml.GetService(request);

            QLog.D("XIAO", result.listAllMyBuckets.GetInfo());
            QLog.D("XIAO", "bucket count: " + result.listAllMyBuckets.buckets.Count);
            Console.WriteLine(result.GetResultInfo());

            //try
            //{
            //    QLog.D("INFO", String.Format("current thread name: {0}-{1}", System.Threading.Thread.CurrentThread.Name, System.Threading.Thread.CurrentThread.ManagedThreadId));
            //    //设置签名有效时长
            //    //request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
            //    //执行请求
            //    GetServiceResult result = cosXml.GetService(request);

            //    QLog.D("XIAO", result.listAllMyBuckets.GetInfo());
            //    QLog.D("XIAO", "bucket count: " + result.listAllMyBuckets.buckets.Count);
            //    Console.WriteLine(result.GetResultInfo());
            //}
            //catch (COSXML.CosException.CosClientException clientEx)
            //{
            //    Console.WriteLine("CosClientException: " + clientEx.StackTrace);
            //}
            //catch (COSXML.CosException.CosServerException serverEx)
            //{
            //    Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            //}
            

        }

        public static void AsyncGetService(CosXml cosXml)
        {
            GetServiceRequest request = new GetServiceRequest();
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
            QLog.D("INFO", String.Format("current thread name: {0}-{1}", System.Threading.Thread.CurrentThread.Name, System.Threading.Thread.CurrentThread.ManagedThreadId));

            cosXml.GetService(request, 
                delegate(COSXML.Model.CosResult cosResult)
                {
                GetServiceResult getServiceResult = cosResult as GetServiceResult;
                QLog.D("XIAO", getServiceResult.GetResultInfo());
                Console.WriteLine(getServiceResult.GetResultInfo());

                }, 
                delegate(COSXML.CosException.CosClientException clientEx, COSXML.CosException.CosServerException serverEx)
                {
                    QLog.D("INFO", String.Format("current thread name: {0}-{1}", System.Threading.Thread.CurrentThread.Name, System.Threading.Thread.CurrentThread.ManagedThreadId));
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
    }
}
