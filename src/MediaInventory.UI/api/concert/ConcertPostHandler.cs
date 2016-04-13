using System;
using AutoMapper;
using MediaInventory.Core.Performance;
using MediaInventory.Core.Venue;
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
        private readonly IArtistService _artistService;

        public ConcertPostHandler(IConcertCreationService concertCreationService, IMapper mapper, IArtistService artistService)
        {
            _concertCreationService = concertCreationService;
            _mapper = mapper;
            _artistService = artistService;
        }

        public ConcertModel Execute(Request request)
        {
            var venue = new Venue();
            return _mapper.Map<ConcertModel>(_concertCreationService.Create(_artistService.GetOrCreateArtist(request.ArtistName),
                request.Date, venue));
        }
    }
}