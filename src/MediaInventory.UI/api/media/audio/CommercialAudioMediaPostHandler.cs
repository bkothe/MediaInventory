using System;
using AutoMapper;
using MediaInventory.Core.Media;

namespace MediaInventory.UI.api.media.audio
{
    public class CommercialAudioMediaPostHandler
    {
        private readonly CommercialAudioMediaCreationService _commercialAudioMediaCreationService;

        public class Request
        {
            public Guid ArtistId { get; set; }
            public string Title { get; set; }
            public MediaFormat MediaFormat { get; set; }
            public DateTime? Released { get; set; }
            public DateTime? Purchased { get; set; }
            public decimal? PurchasePrice { get; set; }
            public string PurchaseLocation { get; set; }
            public int MediaCount { get; set; }
            public string Notes { get; set; }
        }

        public CommercialAudioMediaPostHandler(CommercialAudioMediaCreationService commercialAudioMediaCreationService)
        {
            _commercialAudioMediaCreationService = commercialAudioMediaCreationService;
        }

        public CommercialAudioMediaModel Execute(Request request)
        {
            return Mapper.Map<CommercialAudioMediaModel>(_commercialAudioMediaCreationService.Create(request.ArtistId, request.Title, request.MediaFormat,
                request.Released, request.Purchased, request.PurchasePrice, request.PurchaseLocation, request.MediaCount,
                request.Notes));
        }
    }
}