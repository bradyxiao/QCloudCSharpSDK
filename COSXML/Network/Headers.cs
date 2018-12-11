using System;
using System.Collections.Generic;

using System.Text;
using COSXML.Utils;
using System.Collections;
/**
* Copyright (c) 2018 Tencent Cloud. All rights reserved.
* 11/5/2018 9:15:19 PM
* bradyxiao
*/
namespace COSXML.Network
{
    public sealed class Headers
    {
        private string[] nameAndValues;

        internal Headers(Builder builder)
        {
            this.nameAndValues = builder.namesAndValues.ToArray();
        }

        private Headers(string[] nameAndValues)
        {
            this.nameAndValues = nameAndValues;
        }

        public string Get(string name)
        {
            return Get(nameAndValues, name);
        }

        public int Size()
        {
            return nameAndValues.Length / 2;
        }

        public string Name(int index)
        {
            return nameAndValues[index * 2];
        }

        public string Value(int index)
        {
            return nameAndValues[index * 2 + 1];
        }

        public List<string> Names()
        {
            List<string> result = new List<string>();
            for (int i = 0, size = Size(); i < size; i++)
            {
                result.Add(Name(i));
            }
            return result;

        }

        public List<string> Values(string name)
        {
            List<string> result = null;
            for (int i = 0, size = Size(); i < size; i++)
            {
                if (name.Equals(Name(i), StringComparison.OrdinalIgnoreCase))
                {
                    if (result == null) result = new List<string>();
                    result.Add(Value(i));
                }
            }
            return result;
        }

        public long ByteCount()
        {
            long result = nameAndValues.Length * 2;
            for (int i = 0, size = nameAndValues.Length; i < size; i++)
            {
                result += nameAndValues[i].Length;
            }
            return result;
        }

        public Builder NewBuilder()
        {
            Builder builder = new Builder();
            for (int i = 0, count = this.nameAndValues.Length; i < count; i++)
            {
                builder.namesAndValues.Add(this.nameAndValues[i]);
            }
            return builder;
        }


        private static string Get(string[] nameAndValues, string name)
        {
            for (int i = nameAndValues.Length - 2; i >= 0; i -= 2)
            {
                if(name.Equals(nameAndValues[i], StringComparison.OrdinalIgnoreCase))
                {
                    return nameAndValues[i + 1];
                }
            }
            return null;
        }

        public sealed class Builder
        {
            internal List<string> namesAndValues = new List<string>(20);

            Builder AddLenient(string line)
            {
                int index = line.IndexOf(":", 1);
                if (index != -1)
                {
                    return AddLenient(line.Substring(0, index), line.Substring(index + 1));
                }
                else if (line.StartsWith(":"))
                {
                    // Work around empty header names and header names that start with a
                    // colon (created by old broken SPDY versions of the response cache).
                    return AddLenient("", line.Substring(1)); // Empty header name.
                }
                else
                {
                    return AddLenient("", line); // No header name.
                }
            }

            public Builder Add(string line)
            {
                int index = line.IndexOf(":");
                if (index == -1)
                {
                    throw new ArgumentException("Unexpected header: " + line);
                }
                return Add(line);
            }

            public Builder Add(string name, string value)
            {
                CheckNameAndValue(name, value);
                return AddLenient(name, value);
            }

            public Builder Set(string name, string value)
            {
                CheckNameAndValue(name, value);
                RemoveAll(name);
                AddLenient(name, value);
                return this;
            }

            /**
              * Add a header line without any validation. Only appropriate for headers from the remote peer
              * or cache.
              */
            internal Builder AddLenient(string name, string value)
            {
                namesAndValues.Add(name);
                namesAndValues.Add(value.Trim());
                return this;
            }

            public Builder RemoveAll(string name)
            {
                for (int i = 0; i < namesAndValues.Count; i += 2)
                {
                    if (name.Equals(namesAndValues[i], StringComparison.OrdinalIgnoreCase))
                    {
                        namesAndValues.RemoveAt(i);
                        namesAndValues.RemoveAt(i + 1);
                        i -= 2;
                    }
                }
                return this;
            }

            private void CheckNameAndValue(string name, string value)
            {
                if (name == null) throw new ArgumentNullException("name == null");
                if (name == String.Empty) throw new ArgumentException("name is empty");
                for (int i = 0, length = name.Length; i < length; i++)
                {
                    char c = name[i];
                    if (c <= '\u0020' || c >= '\u007f')
                    {
                        throw new ArgumentException(String.Format(
                            "Unexpected char {0} at {1} in header name: {2}", (int)c, i, name));
                    }
                }
                if (value == null) throw new ArgumentNullException("value for name " + name + " == null");
                for (int i = 0, length = value.Length; i < length; i++)
                {
                    char c = value[i];
                    if ((c <= '\u001f' && c != '\t') || c >= '\u007f')
                    {
                        throw new ArgumentException(String.Format(
                            "Unexpected char {0} at {1} in header value: {2}={3}", (int)c, i, name, value));
                    }
                }
            }

            public string Get(string name)
            {
                for (int i = namesAndValues.Count - 2; i >= 0; i -= 2)
                {
                    if (name.Equals(namesAndValues[i], StringComparison.OrdinalIgnoreCase))
                    {
                        return namesAndValues[i + 1];
                    }
                }
                return null;
            }

            public Headers Build()
            {
                return new Headers(this);
            }

        }

    }
}
