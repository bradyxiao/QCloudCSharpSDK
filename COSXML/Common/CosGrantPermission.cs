using System;
using System.Collections.Generic;

using System.Text;
using COSXML.Common;
using COSXML.Utils;
/**
* Copyright (c) 2018 Tencent Cloud. All rights reserved.
* 11/2/2018 2:30:52 PM
* bradyxiao
*/
namespace COSXML.Common
{
    public enum CosGrantPermission
    {
        [CosValue("READ")]
        READ = 0,

        [CosValue("WRITE")]
        WRITE,

        [CosValue("FULL_CONTROL")]
        FULL_CONTROL
    }
}
