using System;
using System.Collections.Generic;

using System.Text;
using COSXML.Utils;
/**
* Copyright (c) 2018 Tencent Cloud. All rights reserved.
* 11/1/2018 9:48:15 PM
* bradyxiao
*/
namespace COSXML.Common
{
    public enum CosRegion
    {
        [CosValue("ap-beijing-1")]
        AP_Beijing_1,

        [CosValue("ap-beijing")]
        AP_Beijing,

        [CosValue("ap-shanghai")]
        AP_Shanghai,

        [CosValue("ap-guangzhou")]
        AP_Guangzhou,

        [CosValue("ap-guangzhou-2")]
        AP_Guangzhou_2,
        
        [CosValue("ap-chengdu")]
        AP_Chengdu,

        [CosValue("ap-singapore")]
        AP_Singapore,

        [CosValue("ap-hongkong")]
        AP_Hongkong,

        [CosValue("na-toronto")]
        NA_Toronto,

        [CosValue("eu-frankfurt")]
        EU_Frankfurt

    }
}
