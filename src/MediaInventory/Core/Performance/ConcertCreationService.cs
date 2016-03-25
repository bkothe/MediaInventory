using System;
using MediaInventory.Infrastructure.Common.Collections;
using MediaInventory.Infrastructure.Common.Data.Orm;
using MediaInventory.Infrastructure.Common.Exceptions;

namespace MediaInventory.Core.Performance
{
    public interface IConcertCreationService
    {
        Concert Create(Guid artistId, DateTime date, Guid venueId);
    }

    public class ConcertCreationService : IConcertCreationService
    {
        private readonly IRepository<Concert> _concerts;
        private readonly IRepository<Artist.Artist> _artists;
        private readonly IRepository<Venue.Venue> _venues;
        private readonly ConcertValidator _concertValidator;

        public ConcertCreationService(IRepository<Concert> concerts, IRepository<Artist.Artist> artists,
            IRepository<Venue.Venue> venues, ConcertValidator concertValidator)
        {
            _concerts = concerts;
            _artists = artists;
            _venues = venues;
            _concertValidator = concertValidator;
        }

        public Concert Create(Guid artistId, DateTime date, Guid venueId)
        {
            var concert = new Concert
            {
                Artist = _artists.FirstOrThrowNotFound(x => x.Id == artistId, artistId, "Artist"),
                Date = date,
                Venue = _venues.FirstOrThrowNotFound(x => x.Id == venueId, venueId, "Venue")
            };

            _concertValidator.ValidateAndThrow(concert);

            return _concerts.Add(concert);
        }
    }
}