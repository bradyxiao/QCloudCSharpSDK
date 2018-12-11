using System;
using System.Collections.Generic;

using System.Text;

namespace COSXML.Model.Tag
{
    public sealed class LocationConstraint
    {
        public string location;

        public string GetInfo()
        {
            StringBuilder stringBuilder = new StringBuilder("{LocationConstraint:\n");
            stringBuilder.Append("Location:").Append(location).Append("\n");
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }
    }
}
