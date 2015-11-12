using System;
using MediaInventory.Infrastructure.Common.Collections;
using MediaInventory.Infrastructure.Common.Data.Orm;
using MediaInventory.Infrastructure.Common.Exceptions;

namespace MediaInventory.Core.Performance
{
    public interface IConcertModificationService
    {
        Concert Modify(Guid concertId, Action<Concert> modify);
    }

    public class ConcertModificationService : IConcertModificationService
    {
        private readonly IRepository<Concert> _concerts;
        private readonly ConcertValidator _concertValidator;

        public ConcertModificationService(IRepository<Concert> concerts, ConcertValidator concertValidator)
        {
            _concerts = concerts;
            _concertValidator = concertValidator;
        }

        public Concert Modify(Guid concertId, Action<Concert> modify)
        {
            var concert = _concerts.FirstOrThrowNotFound(x => x.Id == concertId, concertId);

            TryModify(concert, modify);

            modify(concert);

            return concert;
        }

        private void TryModify(Concert originalConcert, Action<Concert> modify)
        {
            var concert = originalConcert.Clone();

            modify(concert);

            _concertValidator.ValidateAndThrow(concert);
        }
    }
}