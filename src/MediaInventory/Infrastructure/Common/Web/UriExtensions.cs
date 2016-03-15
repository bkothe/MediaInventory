using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaInventory.Infrastructure.Common.Web
{
    public static class UriExtensions
    {
        public static Uri ToUri(this string uri, params object[] args)
        {
            return new Uri(string.Format(uri, args));
        }
    }
}