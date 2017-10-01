using System;

namespace MediaInventory.Ui.api.venue
{
    public class VenueModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }

    public class VenueModelMapping : Profile
    {
        protected override void Configure()
        {
            CreateMap<Venue, VenueModel>();
        }
    }
}