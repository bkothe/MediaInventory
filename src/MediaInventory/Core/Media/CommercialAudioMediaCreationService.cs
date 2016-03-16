using System;
using MediaInventory.Infrastructure.Common.Collections;
using MediaInventory.Infrastructure.Common.Data.Orm;
using MediaInventory.Infrastructure.Common.Exceptions;

namespace MediaInventory.Core.Media
{
    public interface ICommercialAudioMediaCreationService
    {
        CommercialAudioMedia Create(Guid artistId, string title, MediaFormat mediaFormat, DateTime? released,
            DateTime? purchased, decimal? purchasePrice, string purchaseLocation, int mediaCount, string notes);
    }

    public class CommercialAudioMediaCreationService : ICommercialAudioMediaCreationService
    {
        private readonly IRepository<CommercialAudioMedia> _commercialAudioMediae;
        private readonly IRepository<Artist.Artist> _artists;
        private readonly CommercialAudioMediaValidator _commercialAudioMediaValidator;

        public CommercialAudioMediaCreationService(IRepository<CommercialAudioMedia> commercialAudioMediae, 
            IRepository<Artist.Artist> artists, CommercialAudioMediaValidator commercialAudioMediaValidator)
        {
            _commercialAudioMediae = commercialAudioMediae;
            _artists = artists;
            _commercialAudioMediaValidator = commercialAudioMediaValidator;
        }

        public CommercialAudioMedia Create(Guid artistId, string title, MediaFormat mediaFormat, DateTime? released,
            DateTime? purchased, decimal? purchasePrice, string purchaseLocation, int mediaCount, string notes)
        {
            var commercialAudioMedia = new CommercialAudioMedia {
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

            _commercialAudioMediaValidator.ValidateAndThrow(commercialAudioMedia);

            return _commercialAudioMediae.Add(commercialAudioMedia);
        }
    }
}