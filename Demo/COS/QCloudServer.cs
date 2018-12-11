using COSXML;
using COSXML.Auth;
using COSXML.Common;
using COSXML.Utils;

namespace Demo.COS
{
    class QCloudServer
    {
        //开发者拥有的项目身份识别 ID，用以身份认证
        public string secretId = "XX";

        //开发者拥有的项目身份密钥,用以身份认证
        public string secretKey = "XX";

        //开发者访问 COS 服务时拥有的用户维度唯一资源标识，用以标识资源
        public string appid = "1253960454";

        //存储桶所在的地域
        public string region = EnumUtils.GetValue(CosRegion.AP_Beijing);

        //存储桶
        public string bucketForObjectTest = "netsdk";

        public string bucketForBucketTest = "netsdk-bucket";

        //net sdk 服务类，提供了各个访问cos API 的接口
        public CosXml cosXml;

        private static QCloudServer instance;

        private QCloudServer()
        {
            InitCosXml();
        }

        private void InitCosXml()
        {
            // 初始化 CosXmlConfig
            CosXmlConfig config = new CosXmlConfig.Builder()
            .SetConnectionLimit(1024)
            .SetConnectionTimeoutMs(60000)
            .SetReadWriteTimeoutMs(40000)
            //.IsHttps(true)
            .SetAppid(appid)
            .SetDebugLog(true)
            .SetRegion(region)
            .Build();

            //初始化 QCloudCredentialProvider
            QCloudCredentialProvider cosCredentialProvider = new DefaultQCloudCredentialProvider(secretId, secretKey, 600);

            //初始化 CosXmlServer
            cosXml = new CosXmlServer(config, cosCredentialProvider);
           
        }

        public static QCloudServer GetInstance()
        {
            lock (typeof(QCloudServer))
            {
                if (instance == null)
                {
                    instance = new QCloudServer();
                }
            }
            return instance;
        }
    }
}
