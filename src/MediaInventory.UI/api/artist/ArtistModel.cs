using System;

namespace MediaInventory.Ui.api.artist
{
    public class ArtistModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class ArtistModelMapping : Profile
    {
        protected override void Configure()
        {
            CreateMap<Artist, ArtistModel>();
        }
    }
}