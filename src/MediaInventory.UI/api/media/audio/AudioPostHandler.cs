using System;
using System.Linq;
using AutoMapper;
using MediaInventory.Core.Artist;
using MediaInventory.Core.Media;
using MediaInventory.Infrastructure.Common.Data.Orm;

namespace MediaInventory.UI.api.media.audio
{
    public class AudioPostHandler
    {
        private readonly AudioCreationService _audioCreationService;
        private readonly IMapper _mapper;
        private readonly IRepository<Artist> _artists;
        private readonly IArtistCreationService _artistCreationService;

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

        public AudioPostHandler(AudioCreationService audioCreationService, IMapper mapper,
            IRepository<Artist> artists, IArtistCreationService artistCreationService)
        {
            _audioCreationService = audioCreationService;
            _mapper = mapper;
            _artists = artists;
            _artistCreationService = artistCreationService;
        }

        public AudioModel Execute(Request request)
        {
            var artist = _artists.FirstOrDefault(x => x.Name == request.ArtistName) ?? 
                _artistCreationService.Create(request.ArtistName);

            return _mapper.Map<AudioModel>(_audioCreationService.Create(artist,
                request.Title, request.MediaFormat, request.Released, request.Purchased,
                request.PurchasePrice, request.PurchaseLocation, request.MediaCount, request.Notes));
        }
    }
}