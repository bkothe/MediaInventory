using System.Net;
using System.Web;

namespace MediaInventory.Infrastructure.Common.Web
{
    public interface IHttpStatus
    {
        HttpStatusCode StatusCode { get; set; }
        string StatusDescription { get; set; }
        void Set(HttpStatusCode code, string description);
    }

    public class HttpStatus : IHttpStatus
    {
        public HttpStatusCode StatusCode
        {
            get { return (HttpStatusCode)HttpContext.Current.Response.StatusCode; }
            set
            {
                HttpContext.Current.Response.TrySkipIisCustomErrors = true;
                HttpContext.Current.Response.StatusCode = (int)value;
            }
        }

        public string StatusDescription
        {
            get { return HttpContext.Current.Response.StatusDescription; }
            set { HttpContext.Current.Response.StatusDescription = value; }
        }

        public void Set(HttpStatusCode code, string description)
        {
            StatusCode = code;
            StatusDescription = description;
        }
    }
}
