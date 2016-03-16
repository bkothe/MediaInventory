using MediaInventory.Infrastructure.Common.Data.Orm;
using MediaInventory.Infrastructure.Common.Exceptions;

namespace MediaInventory.Core.Venue
{
    public interface IVenueCreationService
    {
        Venue Create(string name, string city, string state);
    }

    public class VenueCreationService : IVenueCreationService
    {
        private readonly IRepository<Venue> _venues;
        private readonly VenueValidator _venueValidator;

        public VenueCreationService(IRepository<Venue> venues, VenueValidator venueValidator)
        {
            _venues = venues;
            _venueValidator = venueValidator;
        }

        public Venue Create(string name, string city, string state)
        {
            var venue = new Venue
            {
                Name = name,
                City = city,
                State = state
            };

            _venueValidator.ValidateAndThrow(venue);

            return _venues.Add(venue);
        }
    }
}