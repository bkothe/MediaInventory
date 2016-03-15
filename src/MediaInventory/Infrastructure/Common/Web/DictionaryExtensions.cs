using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MediaInventory.Infrastructure.Common.Web.Security;

namespace MediaInventory.Infrastructure.Common.Web
{
    public static class DictionaryExtensions
    {
        public static string ToUrlEncodedString<TKey, TValue>(this IDictionary<TKey, TValue> values)
        {
            return values.ToUrlEncodedString((key, value) => value);
        }

        public static string ToUrlEncodedString<TKey, TValue>(this IDictionary<TKey, TValue> values, Func<TKey, string, string> formatValues)
        {
            if (values == null || !values.Any()) return string.Empty;
            return values.Select(x => x.Key + "=" + formatValues(x.Key, HttpUtility.UrlEncode(x.Value.ToString()))).
                        Aggregate((a, i) => a + ", " + i);
        }

        public static string ToHeaderBlock(this IDictionary<string, string> values)
        {
            if (values == null || !values.Any()) return string.Empty;
            return values
                .MaskAuthorizationHeader()
                .Select(x => x.Key + ": " + x.Value)
                .Aggregate((a, i) => a + "\r\n" + i);
        }
    }
}
