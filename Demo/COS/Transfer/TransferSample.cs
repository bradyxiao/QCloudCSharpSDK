using System;
using COSXML.Transfer;
using COSXML;
using COSXML.Model.Tag;
using COSXML.Model;
using System.Threading;
using COSXML.CosException;
using COSXML.Log;
/**
* Copyright (c) 2018 Tencent Cloud. All rights reserved.
* 12/5/2018 10:13:40 PM
* bradyxiao
*/
namespace Demo.COS.Transfer
{
   public class TransferSample
    {
       private static TransferManager transferManager;
       public static void Init(CosXmlServer cosXmlServer)
       {
           transferManager = new TransferManager(cosXmlServer, new TransferConfig());
       }
       public static void Copy(string bucket, string key, CopySourceStruct copySource)
       {
            COSXMLCopyTask copyTask = new COSXMLCopyTask(bucket, null, key, copySource)
            {
                successCallback = delegate (CosResult cosResult)
               {
                   COSXML.Transfer.COSXMLCopyTask.CopyTaskResult result = cosResult as COSXML.Transfer.COSXMLCopyTask.CopyTaskResult;
                   QLog.D("XIAO", result.GetResultInfo());
                   Console.WriteLine(result.GetResultInfo());
                   Console.WriteLine(String.Format("currentThread id = {0}", Thread.CurrentThread.ManagedThreadId));
               },
                failCallback = delegate (CosClientException clientEx, CosServerException serverEx)
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
               }
            };
           transferManager.Copy(copyTask);
       }

       public static void Download(string bucket, string key, string localDir, string localFileName)
       {
            COSXMLDownloadTask downloadTask = new COSXMLDownloadTask(bucket, null, key, localDir, localFileName)
            {
                progressCallback = delegate (long completed, long total)
                {
                     Console.WriteLine(String.Format("progress = {0} / {1} : {2:##.##}%", completed, total, completed * 100.0 / total));
                },
                successCallback = delegate (CosResult cosResult)
                {
                    COSXML.Transfer.COSXMLDownloadTask.DownloadTaskResult result = cosResult as COSXML.Transfer.COSXMLDownloadTask.DownloadTaskResult;
                    QLog.D("XIAO", result.GetResultInfo());
                    Console.WriteLine(result.GetResultInfo());
                    Console.WriteLine(String.Format("currentThread id = {0}", Thread.CurrentThread.ManagedThreadId));
                },
                failCallback = delegate (CosClientException clientEx, CosServerException serverEx)
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
                }
            };
           transferManager.Download(downloadTask);
       }

       public static void Upload(string bucket, string key, string srcPath, long offset, long sendContentLength)
       {
           COSXMLUploadTask uploadTask = new COSXMLUploadTask(bucket, null, key);
           uploadTask.SetSrcPath(srcPath, offset, sendContentLength);
           uploadTask.progressCallback = delegate(long completed, long total)
           {
               Console.WriteLine(String.Format("progress = {0} / {1} : {2:##.##}%", completed, total, completed * 100.0 / total));
           };
           uploadTask.successCallback = delegate(CosResult cosResult)
           {
               COSXML.Transfer.COSXMLUploadTask.UploadTaskResult result = cosResult as COSXML.Transfer.COSXMLUploadTask.UploadTaskResult;
               QLog.D("XIAO", result.GetResultInfo());
               Console.WriteLine(result.GetResultInfo());
               Console.WriteLine(String.Format("currentThread id = {0}", Thread.CurrentThread.ManagedThreadId));
           };
           uploadTask.failCallback = delegate(CosClientException clientEx, CosServerException serverEx)
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
           };
           transferManager.Upload(uploadTask);
       }
    }
}
