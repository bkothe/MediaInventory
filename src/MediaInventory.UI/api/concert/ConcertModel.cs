using System;
using AutoMapper;
using MediaInventory.Core.Performance;
using MediaInventory.UI.api.venue;

namespace MediaInventory.UI.api.concert
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