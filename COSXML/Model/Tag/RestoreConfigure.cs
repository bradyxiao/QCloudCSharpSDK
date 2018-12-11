using System;
using System.Collections.Generic;

using System.Text;
using COSXML.Common;
using COSXML.Utils;
/**
* Copyright (c) 2018 Tencent Cloud. All rights reserved.
* 11/5/2018 11:46:17 AM
* bradyxiao
*/
namespace COSXML.Model.Tag
{
    public sealed class RestoreConfigure
    {
        public int days;
        public CASJobParameters casJobParameters;

        public string GetInfo()
        {
            StringBuilder stringBuilder = new StringBuilder("{RestoreRequest:\n");
            stringBuilder.Append("Days:").Append(days).Append("\n");
            if(casJobParameters != null)stringBuilder.Append(casJobParameters.GetInfo()).Append("\n");
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }

        public sealed class CASJobParameters
        {
            public Tier tier = Tier.Standard;

            public string GetInfo()
            {
                StringBuilder stringBuilder = new StringBuilder("{CASJobParameters:\n");
                stringBuilder.Append("Tier:").Append(EnumUtils.GetValue(tier)).Append("\n");
                stringBuilder.Append("}");
                return stringBuilder.ToString();
            }
        }

        public enum Tier
        {
            [CosValue("Expedited")]
            Expedited = 0,

            [CosValue("Standard")]
            Standard,

            [CosValue("Bulk")]
            Bulk
        }
    }
}
