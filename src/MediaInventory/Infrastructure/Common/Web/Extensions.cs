using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediaInventory.Infrastructure.Common.Web
{
    public static class Extensions
    {
        public static string ToKeyValueString<TKey, TValue>(this IDictionary<TKey, TValue> values)
        {
            return values.ToKeyValueString((key, value) => value);
        }

        public static string ToKeyValueString<TKey, TValue>(this IDictionary<TKey, TValue> values, Func<TKey, string, string> formatValues)
        {
            if (values == null || !values.Any()) return string.Empty;
            return values.Select(x => $"{x.Key}={formatValues(x.Key, HttpUtility.UrlEncode(x.Value.ToString()))}").
                Aggregate((a, i) => a + ", " + i);
        }

        public static Uri ToUri(this string uri, params object[] args)
        {
            return new Uri(string.Format(uri, args));
        }

        public static EventHandler ToEventHandler(this Action<IHttpContext> handler)
        {
            return (sender, args) => handler(new HttpContextWrapper());
        }
    }
}