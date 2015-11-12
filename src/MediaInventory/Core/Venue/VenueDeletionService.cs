using System;
using MediaInventory.Infrastructure.Common.Data.Orm;

namespace MediaInventory.Core.Venue
{
    public interface IVenueDeletionService
    {
        void Delete(Guid venueId);
    }

    public class VenueDeletionService : IVenueDeletionService
    {
        private readonly IRepository<Venue> _venues;

        public VenueDeletionService(IRepository<Venue> venues)
        {
            _venues = venues;
        }

        public void Delete(Guid venueId)
        {
            _venues.Delete(venueId);
        }
    }
}