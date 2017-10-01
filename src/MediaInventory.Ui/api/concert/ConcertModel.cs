using System;
using MediaInventory.Ui.api.venue;

namespace MediaInventory.Ui.api.concert
{
    public class ConcertModel
    {
        public Guid Id { get; set; }
        public string ArtistName { get; set; }
        public Guid ArtistId { get; set; }
        public VenueModel Venue { get; set; }
        public DateTime Date { get; set; }
    }

    public class ConcertModelMapping : Profile
    {
        protected override void Configure()
        {
            CreateMap<Concert, ConcertModel>();
        }
    }
}