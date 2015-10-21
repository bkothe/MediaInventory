using System;
using FluentValidation;
using MediaInventory.Infrastructure.Framework.Data.Orm;

namespace MediaInventory.Core
{
    public interface IConcertCreationService
    {
        Concert Create(Artist artist, DateTime date);
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

        public Concert Create(Artist artist, DateTime date)
        {
            var concert = new Concert
            {
                Artist = artist,
                Date = date
            };

            _concertValidator.ValidateAndThrow(concert);

            _concerts.Add(concert);

            return concert;
        }
    }
}