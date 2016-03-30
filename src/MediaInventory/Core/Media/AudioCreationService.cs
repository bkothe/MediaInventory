using System;
using MediaInventory.Infrastructure.Common.Collections;
using MediaInventory.Infrastructure.Common.Data.Orm;
using MediaInventory.Infrastructure.Common.Exceptions;

namespace MediaInventory.Core.Media
{
    public interface IAudioCreationService
    {
        Audio Create(Guid artistId, string title, MediaFormat mediaFormat, DateTime? released,
            DateTime? purchased, decimal? purchasePrice, string purchaseLocation, int mediaCount, string notes);
    }

    public class AudioCreationService : IAudioCreationService
    {
        private readonly IRepository<Audio> _audios;
        private readonly IRepository<Artist.Artist> _artists;
        private readonly AudioValidator _audioValidator;

        public AudioCreationService(IRepository<Audio> audios, 
            IRepository<Artist.Artist> artists, AudioValidator audioValidator)
        {
            _audios = audios;
            _artists = artists;
            _audioValidator = audioValidator;
        }

        public Audio Create(Guid artistId, string title, MediaFormat mediaFormat, DateTime? released,
            DateTime? purchased, decimal? purchasePrice, string purchaseLocation, int mediaCount, string notes)
        {
            var audio = new Audio {
                Artist = _artists.FirstOrThrowNotFound(x => x.Id == artistId, artistId, "Artist"),
                Title = title,
                MediaFormat = mediaFormat,
                Released = released,
                Purchased = purchased,
                PurchasePrice = purchasePrice,
                PurchaseLocation = purchaseLocation,
                MediaCount = mediaCount,
                Notes = notes
            };

            _audioValidator.ValidateAndThrow(audio);

            return _audios.Add(audio);
        }
    }
}