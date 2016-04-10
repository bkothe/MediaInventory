using System;
using System.Linq;
using MediaInventory.Core.Artist;
using MediaInventory.Core.Media;
using MediaInventory.Infrastructure.Common.Data.Orm;

namespace MediaInventory.UI.api.media.audio
{
    public class AudioPutHandler
    {
        private readonly IAudioModificationService _audioModificationService;
        private readonly IRepository<Artist> _artists;
        private readonly IArtistCreationService _artistCreationService;

        public class Request
        {
            public Guid AudioId { get; set; }
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

        public AudioPutHandler(IAudioModificationService audioModificationService,
            IRepository<Artist> artists, IArtistCreationService artistCreationService)
        {
            _audioModificationService = audioModificationService;
            _artists = artists;
            _artistCreationService = artistCreationService;
        }

        public void Execute_AudioId(Request request)
        {
            var artist = _artists.FirstOrDefault(y => y.Name == request.ArtistName) ??
                    _artistCreationService.Create(request.ArtistName);

            _audioModificationService.Modify(request.AudioId, x =>
            {
                x.Artist = artist;
                x.Title = request.Title;
                x.MediaFormat = request.MediaFormat;
                x.Released = request.Released;
                x.Purchased = request.Purchased;
                x.PurchasePrice = request.PurchasePrice;
                x.PurchaseLocation = request.PurchaseLocation;
                x.MediaCount = request.MediaCount;
                x.Notes = request.Notes;
            });
        }
    }
}