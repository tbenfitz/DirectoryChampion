using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;

namespace DirectoryAPI.Utilities
{
    public static class WebUtility
    {
        public static string UrlDecode(string encodedValue)
        {            
            return WebUtility.UrlDecode(encodedValue);
        }

        public static string UrlEncode(string value)
        {
            return Uri.EscapeDataString(value);
        }
    }
}
