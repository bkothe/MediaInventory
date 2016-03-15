namespace Reachmail.Infrastructure.Framework.Web
{
    public interface IHttpContext
    {
        IHttpRequest Request { get; }
        IHttpResponse Response { get; }
    }

    public class HttpContextWrapper : IHttpContext
    {
        public HttpContextWrapper()
        {
            Request = new HttpRequestWrapper();
            Response = new HttpResponseWrapper();
        }

        public IHttpRequest Request { get; private set; }
        public IHttpResponse Response { get; private set; }
    }
}
