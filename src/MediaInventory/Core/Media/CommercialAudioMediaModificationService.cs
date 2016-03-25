using System;
using MediaInventory.Infrastructure.Common.Collections;
using MediaInventory.Infrastructure.Common.Data.Orm;
using MediaInventory.Infrastructure.Common.Exceptions;

namespace MediaInventory.Core.Media
{
    public interface ICommercialAudioMediaModificationService
    {
        CommercialAudioMedia Modify(Guid commercialAudioMediaId, Action<CommercialAudioMedia> modify);
    }

    public class CommercialAudioMediaModificationService : ICommercialAudioMediaModificationService
    {
        private readonly IRepository<CommercialAudioMedia> _commercialAudioMediae;
        private readonly CommercialAudioMediaValidator _commercialAudioMediaValidator;

        public CommercialAudioMediaModificationService(IRepository<CommercialAudioMedia> commercialAudioMediae, CommercialAudioMediaValidator commercialAudioMediaValidator)
        {
            _commercialAudioMediae = commercialAudioMediae;
            _commercialAudioMediaValidator = commercialAudioMediaValidator;
        }

        public CommercialAudioMedia Modify(Guid commercialAudioMediaId, Action<CommercialAudioMedia> modify)
        {
            var commercialAudioMedia = _commercialAudioMediae.FirstOrThrowNotFound(x => x.Id == commercialAudioMediaId);

            TryModify(commercialAudioMedia, modify);

            modify(commercialAudioMedia);

            return commercialAudioMedia;
        }

        private void TryModify(CommercialAudioMedia originalCommercialAudioMedia, Action<CommercialAudioMedia> modify)
        {
            var commercialAudioMedia = originalCommercialAudioMedia.Clone();

            modify(commercialAudioMedia);

            _commercialAudioMediaValidator.ValidateAndThrow(commercialAudioMedia);
        }
    }
}