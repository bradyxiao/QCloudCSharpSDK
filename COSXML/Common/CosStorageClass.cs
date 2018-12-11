using System;
using System.Collections.Generic;

using System.Text;
using COSXML.Common;
using COSXML.Utils;
/**
* Copyright (c) 2018 Tencent Cloud. All rights reserved.
* 11/2/2018 1:58:05 PM
* bradyxiao
*/
namespace COSXML.Common
{
    public enum CosStorageClass
    {
        [CosValue("Standard")]
        STANDARD = 0,

        [CosValue("Standard_IA")]
        STANDARD_IA
    }
}
