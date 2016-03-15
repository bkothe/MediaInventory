using System;
using AutoMapper;
using MediaInventory.Core.Artist;
using MediaInventory.Infrastructure.Common.Objects;

namespace MediaInventory.UI.api.artist
{
    public class ArtistModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class ArtistModelMapping : IModelMapping
    {
        public void Initialize()
        {
            Mapper.CreateMap<Artist, ArtistModel>();
        }
    }
}