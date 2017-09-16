

using System.IO;
using System.Web;
using MediaInventory.Infrastructure.Common.Web.Security;

namespace MediaInventory.Infrastructure.Common.Web
{
    public interface IHttpRequest
    {
        IServerVariables ServerVariables { get; }
        IRequestHeaders Headers { get; }
        HttpMethod Method { get; }
        Stream Filter { get; set; }
        Stream InputStream { get; }
        string ContentType { get; set; }
        IBasicAuthentication BasicAuthentication { get; set; }
    }

    public class HttpRequestWrapper : IHttpRequest
    {
        public HttpRequestWrapper()
        {
            Headers = new RequestHeaders();
            ServerVariables = new ServerVariables();
            BasicAuthentication = new BasicAuthentication(new HttpStatus(), Headers, new ResponseHeaders());
        }

        public IServerVariables ServerVariables { get; private set; }
        public IRequestHeaders Headers { get; private set; }
        public HttpMethod Method { get { return HttpContext.Current.Request.HttpMethod.ToHttpMethod(); } }
        public IBasicAuthentication BasicAuthentication { get; set; }

        public Stream Filter
        {
            get { return HttpContext.Current.Request.Filter; }
            set { HttpContext.Current.Request.Filter = value; }
        }

        public Stream InputStream
        {
            get { return HttpContext.Current.Request.InputStream; }
        }

        public string ContentType
        {
            get { return HttpContext.Current.Request.ContentType; }
            set { HttpContext.Current.Request.ContentType = value; }
        }
    }
}
