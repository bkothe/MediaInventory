using AutoMapper;
using MediaInventory.Core.Artist;

namespace MediaInventory.UI.api.artist
{
    public class ArtistPostHandler
    {
        public class Request
        {
            public string Name { get; set; }
        }

        private readonly ArtistCreationService _artistCreationService;

        public ArtistPostHandler(ArtistCreationService artistCreationService)
        {
            _artistCreationService = artistCreationService;
        }

        public ArtistModel Execute(Request request)
        {
            return Mapper.Map<ArtistModel>(_artistCreationService.Create(request.Name));
        }
    }
}