using System;
using System.Collections.Generic;

using System.Text;
using COSXML;
using COSXML.Common;
using COSXML.Log;
using COSXML.CosException;
using System.IO;
using COSXML.Model.Tag;
using COSXML.Transfer;

using COSXML.Utils;
using COSXML.Network;
using Demo.COS.GetServiceSample;
using Demo.COS;
using Demo.COS.Bucket;
using Demo.COS.Object;
using COSXML.Model.Bucket;
using COSXML.Model.Object;
using Demo.COS.Auth;
using System.Threading;
using Demo.COS.Transfer;

namespace Demo
{
    class DemoApp
    {
        private const string TAG = "DemoApp";
        static void Main(string[] args)
        {
         
            QCloudServer instance = QCloudServer.GetInstance();

            //AuthSamples.testCAM(instance.secretId, instance.secretKey);


            //for get service test
            //GetServiceAPI.GetService(instance.cosXml);

            GetServiceAPI.AsyncGetService(instance.cosXml);


            // for bucket test
            //BucketSample.PutBucket(instance.cosXml, instance.bucketForBucketTest);

            //BucketSample.AsynPutBucket(instance.cosXml, instance.bucketForBucketTest);

            //BucketSample.HeadBucket(instance.cosXml, instance.bucketForBucketTest);

            //BucketSample.AsynHeadBucket(instance.cosXml, instance.bucketForBucketTest);

            //BucketSample.GetBucket(instance.cosXml, instance.bucketForBucketTest);

            //BucketSample.AsynGetBucket(instance.cosXml, instance.bucketForBucketTest);

            //BucketSample.GetBuckeLocation(instance.cosXml, instance.bucketForBucketTest);

            //BucketSample.AsynGetBuckeLocation(instance.cosXml, instance.bucketForBucketTest);

            //BucketSample.PutBucketACL(instance.cosXml, instance.bucketForBucketTest);

            //BucketSample.AsynPutBucketACL(instance.cosXml, instance.bucketForBucketTest);

            //BucketSample.GetBucketACL(instance.cosXml, instance.bucketForBucketTest);

            //BucketSample.AsynGetBucketACL(instance.cosXml, instance.bucketForBucketTest);

            //BucketSample.PutBucketCORS(instance.cosXml, instance.bucketForBucketTest);

            //BucketSample.AsynPutBucketCORS(instance.cosXml, instance.bucketForBucketTest);

            //BucketSample.GetBucketCORS(instance.cosXml, instance.bucketForBucketTest);

            //BucketSample.AsynGetBucketCORS(instance.cosXml, instance.bucketForBucketTest);

            //BucketSample.DeleteBucketCORS(instance.cosXml, instance.bucketForBucketTest);

            //BucketSample.AsynDeleteBucketCORS(instance.cosXml, instance.bucketForBucketTest);

            //BucketSample.PutBucketLifeCycle(instance.cosXml, instance.bucketForBucketTest);

            //BucketSample.AsynPutBucketLifeCycle(instance.cosXml, instance.bucketForBucketTest);

            //BucketSample.GetBucketLifeCycle(instance.cosXml, instance.bucketForBucketTest);

            //BucketSample.AsynGetBucketLifeCycle(instance.cosXml, instance.bucketForBucketTest);

            //BucketSample.DeleteBucketLifeCycle(instance.cosXml, instance.bucketForBucketTest);

            //BucketSample.AsynDeleteBucketLifeCycle(instance.cosXml, instance.bucketForBucketTest);

            //BucketSample.PutBucketReplication(instance.cosXml, instance.bucketForBucketTest);

            //BucketSample.AsynPutBucketReplication(instance.cosXml, instance.bucketForBucketTest);

            //BucketSample.GetBucketReplication(instance.cosXml, instance.bucketForBucketTest);

            //BucketSample.AsynGetBucketReplication(instance.cosXml, instance.bucketForBucketTest);

            //BucketSample.DeleteBucketReplication(instance.cosXml, instance.bucketForBucketTest);

            //BucketSample.AsynDeleteBucketReplication(instance.cosXml, instance.bucketForBucketTest);

            //BucketSample.PutBucketVersioning(instance.cosXml, instance.bucketForBucketTest);

            //BucketSample.AsynPutBucketVersioning(instance.cosXml, instance.bucketForBucketTest);

            //BucketSample.GetBucketVersioning(instance.cosXml, instance.bucketForBucketTest);

            //BucketSample.AsynGetBucketVersioning(instance.cosXml, instance.bucketForBucketTest);

            //BucketSample.ListBucketVersions(instance.cosXml, instance.bucketForBucketTest);

            //BucketSample.AsynListBucketVersions(instance.cosXml, instance.bucketForBucketTest);

            //BucketSample.ListMultiUploads(instance.cosXml, instance.bucketForBucketTest);

            //BucketSample.AsynListMultiUploads(instance.cosXml, instance.bucketForBucketTest);

            //BucketSample.DeleteBucket(instance.cosXml, instance.bucketForBucketTest);

            //BucketSample.AsynDeleteBucket(instance.cosXml, instance.bucketForBucketTest);


            //for object test
            string key = "okhttp-3.8.1.jar";
            string srcPath = @"F:\okhttp-3.8.1.jar";

            //ObjectSample.PutObject(instance.cosXml, instance.bucketForObjectTest, key, srcPath);

            //ObjectSample.AsynPutObject(instance.cosXml, instance.bucketForObjectTest, key, srcPath);

            //ObjectSample.HeadObject(instance.cosXml, instance.bucketForObjectTest, key);

            //ObjectSample.AsynHeadObject(instance.cosXml, instance.bucketForObjectTest, key);

            //ObjectSample.PutObjectACL(instance.cosXml, instance.bucketForObjectTest, key);

            //ObjectSample.AsynPutObjectACL(instance.cosXml, instance.bucketForObjectTest, key);

            //ObjectSample.GetObjectACL(instance.cosXml, instance.bucketForObjectTest, key);

            //ObjectSample.AsynGetObjectACL(instance.cosXml, instance.bucketForObjectTest, key);

            string origin = "http://cloud.tencent.com";
            string accessMethod = "PUT";
            //ObjectSample.OptionObject(instance.cosXml, instance.bucketForObjectTest, key, origin, accessMethod);

            //ObjectSample.AsynOptionObject(instance.cosXml, instance.bucketForObjectTest, key, origin, accessMethod);

            key = "cosbrowser-1.0.2-win-32.zip";
            string sourceAppid = instance.appid;
            string sourceBucket = instance.bucketForObjectTest;
            string sourceRegion = instance.region;
            string sourceKey = key;
            COSXML.Model.Tag.CopySourceStruct copySource = new CopySourceStruct(sourceAppid, sourceBucket, sourceRegion, sourceKey);

            //ObjectSample.CopyObject(instance.cosXml, instance.bucketForObjectTest, key + "_copy.txt", copySource);

            //ObjectSample.AsynCopyObject(instance.cosXml, instance.bucketForObjectTest, key + "_copy.txt", copySource);

            //ObjectSample.InitMultiUpload(instance.cosXml, instance.bucketForObjectTest, key);

            //ObjectSample.AsynInitMultiUpload(instance.cosXml, instance.bucketForObjectTest, key);

            string uploadId = "154337751162e84a31bf2b066696d674e58bf0f24b9fab2bdedfd00fa679e0384b0977d82f";

            //ObjectSample.ListParts(instance.cosXml, instance.bucketForObjectTest, key, uploadId);

            //ObjectSample.AsynListParts(instance.cosXml, instance.bucketForObjectTest, key, uploadId);

            int partNumber = 1;
            //ObjectSample.UploadParts(instance.cosXml, instance.bucketForObjectTest, key, uploadId, partNumber, srcPath);

            //ObjectSample.AsynUploadParts(instance.cosXml, instance.bucketForObjectTest, key, uploadId, partNumber, srcPath);

            Dictionary<int, string> partNumberAndEtags = new Dictionary<int,string>();
            string etag = "e1e205658f27e8fafffe1659272a760b";
            partNumberAndEtags.Add(partNumber, etag);

            //ObjectSample.CompleteMultiUpload(instance.cosXml, instance.bucketForObjectTest, key, uploadId, partNumberAndEtags);

            //ObjectSample.AsynCompleteMultiUpload(instance.cosXml, instance.bucketForObjectTest, key, uploadId, partNumberAndEtags);

            //ObjectSample.AbortMultiUpload(instance.cosXml, instance.bucketForObjectTest, key, uploadId);

            //ObjectSample.AsynAbortMultiUpload(instance.cosXml, instance.bucketForObjectTest, key, uploadId);


            //ObjectSample.PartCopyObject(instance.cosXml, instance.bucketForObjectTest, key, copySource, uploadId, partNumber);

            //ObjectSample.AsynPartCopyObject(instance.cosXml, instance.bucketForObjectTest, key, copySource, uploadId, partNumber);

            key = "net_object.txt";
            //ObjectSample.testGetObject(instance.cosXml, instance.bucketForObjectTest, key, @"F:\aws", key);

            //ObjectSample.testAsyncGetObject(instance.cosXml, instance.bucketForObjectTest, key, @"F:\aws", key);

            key = "postic_background.9.png";
            srcPath = @"F:\ic_background.9.png";
            //ObjectSample.PostObject(instance.cosXml, instance.bucketForObjectTest, key, srcPath, null);

            //ObjectSample.AsynPostObject(instance.cosXml, instance.bucketForObjectTest, key, srcPath, null);

            //ObjectSample.RestoreObject(instance.cosXml, instance.bucketForObjectTest, key);

            //ObjectSample.AsynRestoreObject(instance.cosXml, instance.bucketForObjectTest, key);

            List<string> keys = new List<string>();
            keys.Add(key);
            keys.Add("okhttp-3.8.1.jar");
            //ObjectSample.MultiDeleteObject(instance.cosXml, instance.bucketForObjectTest, keys);

            keys.Add("net_object.txtpartCopy.txt");
            //ObjectSample.AsyncMultiDeleteObject(instance.cosXml, instance.bucketForObjectTest, keys);

            //ObjectSample.DeleteObject(instance.cosXml, instance.bucketForObjectTest, "Android开发艺术探索.pdf");

            //ObjectSample.AsynDeleteObject(instance.cosXml, instance.bucketForObjectTest, "Android开发艺术探索2.pdf");



            //transfer
            TransferSample.Init((CosXmlServer)instance.cosXml);
            key = "part_upload2_cosbrowser-1.0.2-win-32.zip";
            //TransferSample.Copy(instance.bucketForObjectTest, key, copySource);
            key = "multi_issue.rar";
            //TransferSample.Download(instance.bucketForObjectTest, key, @"F:\Download1", "test2.rar");
            srcPath = @"F:\工具资料\androidStudio_中文输入法问题issue包.rar";
           
            //TransferSample.Upload(instance.bucketForObjectTest, key, srcPath, 0, -1); 

            //testMd5(@"F:\Download1\test2.rar", srcPath);

            Console.ReadKey();
        }

        public static void testMd5(string path1, string path2)
        {
            Stream fileInputStream = new FileStream(path1, FileMode.Open, FileAccess.Read);
            string md51 = DigestUtils.GetMd5ToBase64(fileInputStream);
            fileInputStream.Close();

            Stream fileInputStream2 = new FileStream(path2, FileMode.Open, FileAccess.Read);
            string md52 = DigestUtils.GetMd5ToBase64(fileInputStream2);
            fileInputStream2.Close();

            Console.WriteLine(md51);
            Console.WriteLine(md52);
            Console.WriteLine(md51.CompareTo(md52));
        }
        

        public static void test(CosXml cosXml) 
        {
            List<string> keys = new List<string>();
            string bucket = "android-demo-ap-guangzhou";
            GetBucketRequest getBucket = new GetBucketRequest(bucket);
            getBucket.Region = EnumUtils.GetValue(CosRegion.AP_Guangzhou);
            getBucket.SetSign(COSXML.Utils.TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
            GetBucketResult result = cosXml.GetBucket(getBucket);
            Console.WriteLine(result.GetResultInfo());
            foreach (ListBucket.Contents content in result.listBucket.contentsList)
            {
                if (content != null)
                {
                    keys.Add(content.key);
                }
            }
            if (keys.Count > 0)
            {
                COSXML.Model.Object.DeleteMultiObjectRequest deleteMults = new COSXML.Model.Object.DeleteMultiObjectRequest(bucket);
                deleteMults.SetSign(COSXML.Utils.TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
                deleteMults.Region = EnumUtils.GetValue(CosRegion.AP_Guangzhou);
                deleteMults.SetObjectKeys(keys);
                COSXML.Model.Object.DeleteMultiObjectResult deleteMultisResult = cosXml.DeleteMultiObjects(deleteMults);
                Console.WriteLine(deleteMultisResult.GetResultInfo());
            }

            ListMultiUploadsRequest listMultis = new ListMultiUploadsRequest(bucket);
            listMultis.Region = EnumUtils.GetValue(CosRegion.AP_Guangzhou);
            listMultis.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
            ListMultiUploadsResult listMultisResult = cosXml.ListMultiUploads(listMultis);
            Console.WriteLine(listMultisResult.GetResultInfo());

            if (listMultisResult.listMultipartUploads.uploads.Count > 0)
            {
                foreach(ListMultipartUploads.Upload upload in listMultisResult.listMultipartUploads.uploads)
                {
                    if (upload == null) continue;
                    string key = upload.key;
                    string uploadId = upload.uploadID;

                    COSXML.Model.Object.AbortMultiUploadRequest abort = new COSXML.Model.Object.AbortMultiUploadRequest(bucket,
                        key, uploadId);
                    abort.Region = EnumUtils.GetValue(CosRegion.AP_Guangzhou);
                    abort.SetSign(COSXML.Utils.TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
                    COSXML.Model.Object.AbortMultiUploadResult abortResult = cosXml.AbortMultiUpload(abort);
                }
               
            }
        }

    }

}
