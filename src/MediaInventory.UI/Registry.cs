using AutoMapper;
using MediaInventory.Core.Artist;
using MediaInventory.Core.Media;
using MediaInventory.Core.Performance;
using MediaInventory.Core.Venue;
using MediaInventory.Infrastructure.Common.Web;
using MediaInventory.UI.Core;
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

            //ui core
            For<IArtistResolverService>().Use<ArtistResolverService>();
            For<IVenueResolverService>().Use<VenueResolverService>();

            // domain
            For<IArtistCreationService>().Use<ArtistCreationService>();
            For<IAudioCreationService>().Use<AudioCreationService>();
            For<IAudioModificationService>().Use<AudioModificationService>();
            For<IConcertCreationService>().Use<ConcertCreationService>();
            For<IVenueCreationService>().Use<VenueCreationService>();
        }
    }
}