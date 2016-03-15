﻿using System;
using AutoMapper;
using MediaInventory.Core.Performance;
using MediaInventory.Infrastructure.Common.Objects;
using MediaInventory.UI.api.artist;
using MediaInventory.UI.api.venue;

namespace MediaInventory.UI.api.concert
{
    public class ConcertModel
    {
        public Guid Id { get; set; }
        public ArtistModel Artist { get; set; }
        public VenueModel Venue { get; set; }
        public DateTime Date { get; set; }
    }

    public class ConcertModelMapping : IModelMapping
    {
        public void Initialize()
        {
            Mapper.CreateMap<Concert, ConcertModel>();
        }
    }
}