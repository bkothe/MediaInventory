using System;
using AutoMapper;
using MediaInventory.Core.Media;
using MediaInventory.UI.Core;

namespace MediaInventory.UI.api.media.audio
{
    public class AudioPostHandler
    {
        private readonly IAudioCreationService _audioCreationService;
        private readonly IMapper _mapper;
        private readonly IArtistService _artistService;

        public class Request
        {
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

        public AudioPostHandler(IAudioCreationService audioCreationService, IMapper mapper, IArtistService artistService)
        {
            _audioCreationService = audioCreationService;
            _mapper = mapper;
            _artistService = artistService;
        }

        public AudioModel Execute(Request request)
        {
            return _mapper.Map<AudioModel>(_audioCreationService.Create(_artistService.GetOrCreateArtist(request.ArtistName),
                request.Title, request.MediaFormat, request.Released, request.Purchased,
                request.PurchasePrice, request.PurchaseLocation, request.MediaCount, request.Notes));
        }
    }
}