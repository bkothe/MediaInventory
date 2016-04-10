using System;
using MediaInventory.Infrastructure.Common.Data.Orm;
using MediaInventory.Infrastructure.Common.Exceptions;

namespace MediaInventory.Core.Media
{
    public interface IAudioCreationService
    {
        Audio Create(Artist.Artist artist, string title, MediaFormat mediaFormat, DateTime? released,
            DateTime? purchased, decimal? purchasePrice, string purchaseLocation, int mediaCount, string notes);
    }

    public class AudioCreationService : IAudioCreationService
    {
        private readonly IRepository<Audio> _audios;
        private readonly AudioValidator _audioValidator;

        public AudioCreationService(IRepository<Audio> audios, AudioValidator audioValidator)
        {
            _audios = audios;
            _audioValidator = audioValidator;
        }

        public Audio Create(Artist.Artist artist, string title, MediaFormat mediaFormat, DateTime? released,
            DateTime? purchased, decimal? purchasePrice, string purchaseLocation, int mediaCount, string notes)
        {
            var audio = new Audio {
                Artist = artist,
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