using System;
using AutoMapper;
using MediaInventory.Core.Performance;
using MediaInventory.Infrastructure.Common.Objects;

namespace MediaInventory.UI.api.concert
{
    public class ConcertEnumerationModel
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string ArtistName { get; set; }
        public string VenueName { get; set; }
        public string VenueCity { get; set; }
        public string VenueState { get; set; }
    }

    public class ConcertEnumerationModelMapping : IModelMapping
    {
        public void Initialize()
        {
            Mapper.CreateMap<Concert, ConcertEnumerationModel>()
                .ForMember(x => x.ArtistName, x => x.MapFrom(y => y.Artist.Name))
                .ForMember(x => x.VenueName, x => x.MapFrom(y => y.Venue.Name))
                .ForMember(x => x.VenueCity, x => x.MapFrom(y => y.Venue.City))
                .ForMember(x => x.VenueState, x => x.MapFrom(y => y.Venue.State));
        }
    }
}