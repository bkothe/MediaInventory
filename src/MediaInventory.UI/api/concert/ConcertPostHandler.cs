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

        public ConcertPostHandler(ConcertCreationService concertCreationService)
        {
            _concertCreationService = concertCreationService;
        }

        public ConcertModel Execute(Request request)
        {
            return Mapper.Map<ConcertModel>(_concertCreationService.Create(request.ArtistId, request.Date, request.VenueId));
        }
    }
}