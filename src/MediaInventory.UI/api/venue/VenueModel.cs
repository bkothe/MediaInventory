using System;
using AutoMapper;
using MediaInventory.Core.Venue;
using MediaInventory.Infrastructure.Common.Objects;

namespace MediaInventory.UI.api.venue
{
    public class VenueModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }

    public class VenueModelMapping : IModelMapping
    {
        public void Initialize()
        {
            Mapper.CreateMap<Venue, VenueModel>();
        }
    }
}