using System;

namespace MediaInventory.Ui.api.venue
{
    public class VenuePutHandler
    {
        private readonly VenueModificationService _venueModificationService;

        public class Request
        {
            public Guid VenueId { get; set; }
            public string Name { get; set; }
            public string City { get; set; }
            public string State { get; set; }
        }

        public VenuePutHandler(VenueModificationService venueModificationService)
        {
            _venueModificationService = venueModificationService;
        }

        public void Execute_VenueId(Request request)
        {
            _venueModificationService.Modify(request.VenueId, x =>
            {
                x.Name = request.Name;
                x.City = request.City;
                x.State = request.State;
            });
        }
    }
}