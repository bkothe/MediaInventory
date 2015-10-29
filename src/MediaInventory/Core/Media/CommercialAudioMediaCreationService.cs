using System;
using MediaInventory.Infrastructure.Framework.Data.Orm;

namespace MediaInventory.Core.Media
{
    public interface ICommercialAudioMediaCreationService
    {
        CommercialAudioMedia Create(Artist.Artist artist, string title, MediaFormat mediaFormat, DateTime? released, DateTime? purchased, decimal? purchasePrice, string purchaseLocation, int mediaCount, string notes);
    }

    public class CommercialAudioMediaCreationService : ICommercialAudioMediaCreationService
    {
        private readonly IRepository<CommercialAudioMedia> _commercialAudioMediae;

        public CommercialAudioMediaCreationService(IRepository<CommercialAudioMedia> commercialAudioMediae)
        {
            _commercialAudioMediae = commercialAudioMediae;
        }

        public CommercialAudioMedia Create(Artist.Artist artist, string title, MediaFormat mediaFormat, DateTime? released,
            DateTime? purchased, decimal? purchasePrice, string purchaseLocation, int mediaCount, string notes)
        {
            var commercialAudioMedia = new CommercialAudioMedia {
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

            _commercialAudioMediae.Add(commercialAudioMedia);

            return commercialAudioMedia;
        }
    }
}