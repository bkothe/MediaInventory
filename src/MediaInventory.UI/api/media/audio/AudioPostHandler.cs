using System;
using AutoMapper;
using MediaInventory.Core.Media;

namespace MediaInventory.UI.api.media.audio
{
    public class AudioPostHandler
    {
        private readonly AudioCreationService _audioCreationService;
        private readonly IMapper _mapper;

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

        public AudioPostHandler(AudioCreationService audioCreationService, IMapper mapper)
        {
            _audioCreationService = audioCreationService;
            _mapper = mapper;
        }

        public AudioModel Execute(Request request)
        {
            return _mapper.Map<AudioModel>(_audioCreationService.Create(request.ArtistId, request.Title, request.MediaFormat,
                request.Released, request.Purchased, request.PurchasePrice, request.PurchaseLocation, request.MediaCount,
                request.Notes));
        }
    }
}