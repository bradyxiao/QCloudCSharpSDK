using System;
using System.Collections.Generic;
using System.Text;
/**
* Copyright (c) 2018 Tencent Cloud. All rights reserved.
* 11/16/2018 10:09:57 PM
* bradyxiao
*/
namespace COSXML.Model.Tag
{
    public sealed class Range
    {
        private long start;
        private long end;

        public Range(long start, long end)
        {
            this.start = start;
            this.end = end;
        }

        public long Start
        {
            get { return start; }
            set { start = value; }
        }

        public long End
        {
            get { return end; }
            set { end = value; }
        }

        public string GetRange()
        {
            return String.Format("bytes={0}-{1}", (start <= 0 ? 0 : start), (end <= 0 ? "" : end.ToString())); 
        }
    }
}
