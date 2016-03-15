using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Web;

namespace MediaInventory.Infrastructure.Common.Web
{
    public interface IRequestHeaders : IDictionary<string, string> { }

    public class RequestHeaders : NameValueCollectionAdapterBase, IRequestHeaders
    {
        private RequestHeaders(NameValueCollection headers) : base(() => headers) { }
        private RequestHeaders(WebHeaderCollection headers) : base(() => headers) { }
        public RequestHeaders() : base(() => HttpContext.Current != null ? HttpContext.Current.Request.Headers : null) { }

        public static RequestHeaders CreateFrom(NameValueCollection headers)
        {
            return new RequestHeaders(headers);
        }

        public static RequestHeaders CreateFrom(WebHeaderCollection headers)
        {
            return new RequestHeaders(headers);
        }

        public override string ToString()
        {
            return this.ToHeaderBlock();
        }
    }
}