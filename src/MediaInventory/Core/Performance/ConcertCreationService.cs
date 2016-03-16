using System;
using MediaInventory.Infrastructure.Common.Data.Orm;
using MediaInventory.Infrastructure.Common.Exceptions;

namespace MediaInventory.Core.Performance
{
    public interface IConcertCreationService
    {
        Concert Create(Artist.Artist artist, DateTime date, Venue.Venue venue);
    }

    public class ConcertCreationService : IConcertCreationService
    {
        private readonly IRepository<Concert> _concerts;
        private readonly ConcertValidator _concertValidator;

        public ConcertCreationService(IRepository<Concert> concerts, ConcertValidator concertValidator)
        {
            _concerts = concerts;
            _concertValidator = concertValidator;
        }

        public Concert Create(Artist.Artist artist, DateTime date, Venue.Venue venue)
        {
            var concert = new Concert
            {
                Artist = artist,
                Date = date,
                Venue = venue
            };

            _concertValidator.ValidateAndThrow(concert);

            return _concerts.Add(concert);
        }
    }
}