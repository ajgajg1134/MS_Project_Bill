using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace billc
{
    public static class TypeMethods
    {
        public static bool IsList(this string s)
        {
            return s.Substring(0, 5) == "List<";
        }

        public static string GetListType(this string s)
        {
            string removeBeg = s.Substring(5);
            return removeBeg.Substring(0, removeBeg.Length - 1);
        }

        public static bool IsFieldIdentifier(this string s)
        {
            return s.Contains(".");
        }

        public static string GetBeforeField(this string s)
        {
            int index = s.IndexOf('.');
            return s.Substring(0, index);
        }

        public static string GetField(this string s)
        {
            int index = s.IndexOf('.');
            return s.Substring(index + 1);
        }
    }
}
