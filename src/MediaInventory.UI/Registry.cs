using AutoMapper;
using MediaInventory.Core.Artist;
using MediaInventory.Infrastructure.Common.Web;
using StructureMap.Graph;
using Mapper = MediaInventory.Infrastructure.Common.Objects.Mapper;

namespace MediaInventory.UI
{
    public class Registry : StructureMap.Configuration.DSL.Registry
    {
        public Registry()
        {
            // infrastructure
            For<IMapper>().Use<Mapper>();
            Scan(x =>
            {
                x.TheCallingAssembly();
                x.AddAllTypesOf<Profile>();
            });

            // web
            For<IHttpStatus>().Use<HttpStatus>();
            For<IRequestHeaders>().Use<RequestHeaders>();
            For<IResponseHeaders>().Use<ResponseHeaders>();

            // domain
            For<IArtistCreationService>().Use<ArtistCreationService>();
        }
    }
}