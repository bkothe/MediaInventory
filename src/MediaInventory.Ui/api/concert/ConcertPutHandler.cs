using System;

namespace MediaInventory.Ui.api.concert
{
    public class ConcertPutHandler
    {
        private readonly ConcertModificationService _concertModificationService;
        private readonly IRepository<Artist> _artists;
        private readonly IRepository<Venue> _venues;

        public class Request
        {
            public Guid ConcertId { get; set; }
            public Guid ArtistId { get; set; }
            public Guid VenueId { get; set; }
            public DateTime Date { get; set; }
        }

        public ConcertPutHandler(ConcertModificationService concertModificationService,
            IRepository<Artist> artists, IRepository<Venue> venues)
        {
            _concertModificationService = concertModificationService;
            _artists = artists;
            _venues = venues;
        }

        public void Execute_ConcertId(Request request)
        {
            _concertModificationService.Modify(request.ConcertId, x =>
            {
                x.Artist = _artists.FirstOrThrowNotFound(y => y.Id == request.ArtistId, request.ArtistId, "Artist");
                x.Venue = _venues.FirstOrThrowNotFound(y => y.Id == request.VenueId, request.VenueId, "Venue");
                x.Date = request.Date;
            });
        }
    }
}