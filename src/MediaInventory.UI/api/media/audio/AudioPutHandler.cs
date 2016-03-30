using System;
using MediaInventory.Core.Artist;
using MediaInventory.Core.Media;
using MediaInventory.Infrastructure.Common.Collections;
using MediaInventory.Infrastructure.Common.Data.Orm;

namespace MediaInventory.UI.api.media.audio
{
    public class AudioPutHandler
    {
        private readonly AudioModificationService _audioModificationService;
        private readonly IRepository<Artist> _artists;

        public class Request
        {
            public Guid AudioId { get; set; }
            public Guid ArtistId { get; set; }
            public string Title { get; set; }
            public int MediaFormat { get; set; }
            public DateTime? Released { get; set; }
            public DateTime? Purchased { get; set; }
            public decimal? PurchasePrice { get; set; }
            public string PurchaseLocation { get; set; }
            public int MediaCount { get; set; }
            public string Notes { get; set; }
        }

        public AudioPutHandler(AudioModificationService audioModificationService, IRepository<Artist> artists)
        {
            _audioModificationService = audioModificationService;
            _artists = artists;
        }

        public void Execute_AudioId(Request request)
        {
            _audioModificationService.Modify(request.AudioId, x =>
            {
                x.Artist = _artists.FirstOrThrowNotFound(y => y.Id == request.ArtistId, request.ArtistId, "Artist");
                x.Title = request.Title;
                x.MediaFormat = (MediaFormat)request.MediaFormat;
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