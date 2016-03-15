using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;

namespace MediaInventory.Infrastructure.Common.Web
{
    public interface IResponseHeaders : IDictionary<string, string> { }

    public class ResponseHeaders : NameValueCollectionAdapterBase, IResponseHeaders
    {
        private ResponseHeaders(NameValueCollection headers) : base(() => headers) { }
        public ResponseHeaders() : base(() => HttpContext.Current != null ? HttpContext.Current.Response.Headers : null) { }

        public static ResponseHeaders CreateFrom(NameValueCollection headers)
        {
            return new ResponseHeaders(headers);
        }

        public override string ToString()
        {
            return this.ToHeaderBlock();
        }
    }
}