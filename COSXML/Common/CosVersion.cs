﻿using System;
using System.Collections.Generic;

using System.Text;
/**
* Copyright (c) 2018 Tencent Cloud. All rights reserved.
* 11/2/2018 2:34:04 PM
* bradyxiao
*/
namespace COSXML.Common
{
    public sealed class CosVersion
    {
        public static string SDKVersion = "5.4.2.0";

        public static string GetUserAgent()
        {
            StringBuilder userAgent = new StringBuilder();
            userAgent.Append("cos-net-sdk").Append('.')
                .Append(SDKVersion);
            return userAgent.ToString();
        }

        //public static string GetOsVersion()
        //{
        //    try
        //    {
        //        var os = Environment.OSVersion;
        //        return "windows " + os.Version.Major + "." + os.Version.Minor;
        //    }
        //    catch (InvalidOperationException)
        //    {
        //        return "Unknown OSVersion";
        //    }
        //}

        //public static string GetOsArchitecture()
        //{
        //    return (IntPtr.Size == 8) ? "x86_64" : "x86";
        //}
    }
}
