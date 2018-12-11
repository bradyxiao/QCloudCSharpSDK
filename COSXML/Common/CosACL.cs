using System;
using System.Collections.Generic;

using System.Text;
using COSXML.Utils;
/**
* Copyright (c) 2018 Tencent Cloud. All rights reserved.
* 11/2/2018 1:53:35 PM
* bradyxiao
*/
namespace COSXML.Common
{
    public enum CosACL
    {
        [CosValue("private")]
        PRIVATE = 0,

        [CosValue("public-read")]
        PUBLIC_READ,

        [CosValue("public-read-write")]
        PUBLIC_READ_WRITE,
    }
}
