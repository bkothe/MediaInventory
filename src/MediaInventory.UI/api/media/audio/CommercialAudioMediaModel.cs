using System;
using AutoMapper;
using MediaInventory.Core.Media;
using MediaInventory.Infrastructure.Common.Objects;

namespace MediaInventory.UI.api.media.audio
{
    public class CommercialAudioMediaModel
    {
        public Guid Id { get; set; }
        public Guid ArtistId { get; set; }
        public string ArtistName { get; set; }
        public string Title { get; set; }
        public MediaFormat MediaFormat { get; set; }
        public DateTime? Released { get; set; }
        public DateTime? Purchased { get; set; }
        public decimal? PurchasePrice { get; set; }
        public string PurchaseLocation { get; set; }
        public int MediaCount { get; set; }
        public string Notes { get; set; }
    }

    public class CommercialAudioMediaModelMapping : IModelMapping
    {
        public void Initialize()
        {
            Mapper.CreateMap<CommercialAudioMedia, CommercialAudioMediaModel>();
        }
    }
}