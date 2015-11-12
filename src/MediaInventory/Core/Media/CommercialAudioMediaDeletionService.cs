using System;
using MediaInventory.Infrastructure.Common.Data.Orm;

namespace MediaInventory.Core.Media
{
    public interface ICommercialMediaDeletionService
    {
        void Delete(Guid commercialAudioMediaId);
    }

    public class CommercialAudioMediaDeletionService : ICommercialMediaDeletionService
    {
        private readonly IRepository<CommercialAudioMedia> _commercialAudioMediae;

        public CommercialAudioMediaDeletionService(IRepository<CommercialAudioMedia> commercialAudioMediae)
        {
            _commercialAudioMediae = commercialAudioMediae;
        }

        public void Delete(Guid commercialAudioMediaId)
        {
            _commercialAudioMediae.Delete(commercialAudioMediaId);
        }
    }
}