using System;
using MediaInventory.Infrastructure.Common.Data.Orm;

namespace MediaInventory.Core.Performance
{
    public interface IConcertDeletionService
    {
        void Delete(Guid concertId);
    }

    public class ConcertDeletionService : IConcertDeletionService
    {
        private readonly IRepository<Concert> _concerts;

        public ConcertDeletionService(IRepository<Concert> concerts)
        {
            _concerts = concerts;
        }

        public void Delete(Guid concertId)
        {
            _concerts.Delete(concertId);
        }
    }
}