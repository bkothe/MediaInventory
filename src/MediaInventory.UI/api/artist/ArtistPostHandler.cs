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
        private readonly IMapper _mapper;

        public ArtistPostHandler(ArtistCreationService artistCreationService, IMapper mapper)
        {
            _artistCreationService = artistCreationService;
            _mapper = mapper;
        }

        public ArtistModel Execute(Request request)
        {
            return _mapper.Map<ArtistModel>(_artistCreationService.Create(request.Name));
        }
    }
}