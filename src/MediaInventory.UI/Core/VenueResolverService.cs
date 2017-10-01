namespace MediaInventory.Ui.Core
{
    public interface IVenueResolverService
    {
        Venue ResolveVenue(string artistName);
    }

    public class VenueResolverService : IVenueResolverService
    {
        private readonly IRepository<Venue> _venues;
        private readonly IVenueCreationService _venueCreationService;

        public VenueResolverService(IRepository<Venue> venues, IVenueCreationService venueCreationService)
        {
            _venues = venues;
            _venueCreationService = venueCreationService;
        }

        public Venue ResolveVenue(string venueName)
        {
            return _venues.FirstOrDefault(x => x.Name == venueName) ??
                _venueCreationService.Create(venueName);
        }
    }
}