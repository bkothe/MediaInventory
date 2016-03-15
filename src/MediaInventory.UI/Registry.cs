using MediaInventory.Infrastructure.Common.Web;

namespace MediaInventory.UI
{
    public class Registry : StructureMap.Configuration.DSL.Registry
    {
        public Registry()
        {
            // Web
            
            For<IHttpStatus>().Use<HttpStatus>();
            For<IRequestHeaders>().Use<RequestHeaders>();
            For<IResponseHeaders>().Use<ResponseHeaders>();
        }
    }
}