using System;
using System.Collections.Generic;

using System.Text;
using COSXML.Common;
using System.IO;
using COSXML.CosException;

namespace COSXML.Model.Object
{
    public sealed class AppendObjectRequest : ObjectRequest
    {
        private long position = 0;
        private string srcPath;
        private byte[] data;
       

        private AppendObjectRequest(string bucket, string cosPath, long position)
            : base(bucket, cosPath)
        {
            this.position = position;
            this.method = CosRequestMethod.POST;
            this.queryParameters.Add("append", null);
        }

        public AppendObjectRequest(string bucket, string cosPath, long position, string srcPath)
            : this(bucket, cosPath, position)
        {
            this.srcPath = srcPath;

        }

        public AppendObjectRequest(string bucket, string cosPath, long position, byte[] data)
            : this(bucket, cosPath, position)
        {
            this.data = data;
        }


        public override void CheckParameters()
        {
            base.CheckParameters();
        }

    }
}
