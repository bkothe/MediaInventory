using System;
using FluentValidation;
using MediaInventory.Infrastructure.Framework.Collections;
using MediaInventory.Infrastructure.Framework.Data.Orm;

namespace MediaInventory.Core.Venue
{
    public interface IVenueModificationService
    {
        Venue Modify(Guid id, Action<Venue> modify);
    }

    public class VenueModificationService : IVenueModificationService
    {
        private readonly IRepository<Venue> _venues;
        private readonly VenueValidator _venueValidator;

        public VenueModificationService(IRepository<Venue> venues, VenueValidator venueValidator)
        {
            _venues = venues;
            _venueValidator = venueValidator;
        }

        public Venue Modify(Guid id, Action<Venue> modify)
        {
            var venue = _venues.FirstOrThrowNotFound(x => x.Id == id, id);

            TryModify(venue, modify);

            modify(venue);

            return venue;
        }

        private void TryModify(Venue originalVenue, Action<Venue> modify)
        {
            var venue = originalVenue.Clone();

            modify(venue);

            _venueValidator.ValidateAndThrow(venue);
        }
    }
}
