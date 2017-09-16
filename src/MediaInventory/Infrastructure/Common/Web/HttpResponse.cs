using System.IO;
using System.Web;

namespace MediaInventory.Infrastructure.Common.Web
{
    public interface IHttpResponse
    {
        IResponseHeaders Headers { get; }
        Stream Filter { get; set; }
        Stream OutputStream { get; }
        string ContentType { get; set; }
        IHttpStatus Status { get; }
    }

    public class HttpResponseWrapper : IHttpResponse
    {
        public HttpResponseWrapper()
        {
            Headers = new ResponseHeaders();
            Status = new HttpStatus();
        }

        public IResponseHeaders Headers { get; private set; }
        public IHttpStatus Status { get; private set; }

        public Stream Filter
        {
            get { return HttpContext.Current.Response.Filter; }
            set { HttpContext.Current.Response.Filter = value; }
        }

        public Stream OutputStream
        {
            get { return HttpContext.Current.Response.OutputStream; }
        }

        public string ContentType
        {
            get { return HttpContext.Current.Response.ContentType; }
            set { HttpContext.Current.Response.ContentType = value; }
        }
    }
}
