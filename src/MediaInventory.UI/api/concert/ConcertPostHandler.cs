using System;
using AutoMapper;
using MediaInventory.Core.Performance;

namespace MediaInventory.UI.api.concert
{
    public class ConcertPostHandler
    {
        public class Request
        {
            public Guid ArtistId { get; set; }
            public DateTime Date { get; set; }
            public Guid VenueId { get; set; }
        }

        private readonly ConcertCreationService _concertCreationService;
        private readonly IMapper _mapper;

        public ConcertPostHandler(ConcertCreationService concertCreationService, IMapper mapper)
        {
            _concertCreationService = concertCreationService;
            _mapper = mapper;
        }

        public ConcertModel Execute(Request request)
        {
            return _mapper.Map<ConcertModel>(_concertCreationService.Create(request.ArtistId, request.Date, request.VenueId));
        }
    }
}