using System;
using AutoMapper;
using MediaInventory.Core.Performance;
using MediaInventory.UI.Core;

namespace MediaInventory.UI.api.concert
{
    public class ConcertPostHandler
    {
        public class Request
        {
            public string ArtistName { get; set; }
            public DateTime Date { get; set; }
            public string VenueName { get; set; }
        }

        private readonly IConcertCreationService _concertCreationService;
        private readonly IMapper _mapper;
        private readonly IArtistResolverService _artistResolverService;
        private readonly IVenueResolverService _venueResolverService;

        public ConcertPostHandler(IConcertCreationService concertCreationService, IMapper mapper, IArtistResolverService artistResolverService, IVenueResolverService venueResolverService)
        {
            _concertCreationService = concertCreationService;
            _mapper = mapper;
            _artistResolverService = artistResolverService;
            _venueResolverService = venueResolverService;
        }

        public ConcertModel Execute(Request request)
        {
            return _mapper.Map<ConcertModel>(_concertCreationService.Create(_artistResolverService.ResolveArtist(request.ArtistName),
                request.Date, _venueResolverService.ResolveVenue(request.VenueName)));
        }
    }
}