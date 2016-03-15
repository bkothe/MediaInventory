using AutoMapper;
using MediaInventory.Core.Venue;

namespace MediaInventory.UI.api.venue
{
    public class VenuePostHandler
    {
        public class Request
        {
            public string Name { get; set; }
            public string City { get; set; }
            public string State { get; set; }
        }

        private readonly VenueCreationService _venueCreationService;

        public VenuePostHandler(VenueCreationService venueCreationService)
        {
            _venueCreationService = venueCreationService;
        }

        public VenueModel Execute(Request request)
        {
            return Mapper.Map<VenueModel>(_venueCreationService.Create(request.Name, request.City, request.State));
        }
    }
}