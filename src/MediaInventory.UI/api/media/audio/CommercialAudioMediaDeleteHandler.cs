using MediaInventory.Core.Media;

namespace MediaInventory.UI.api.media.audio
{
    public class CommercialAudioMediaDeleteHandler
    {
        private readonly CommercialAudioMediaDeletionService _commercialAudioMediaDeletionService;

        public CommercialAudioMediaDeleteHandler(CommercialAudioMediaDeletionService commercialAudioMediaDeletionService)
        {
            _commercialAudioMediaDeletionService = commercialAudioMediaDeletionService;
        }

        public void Execute_Id(RequestGuidId request)
        {
            _commercialAudioMediaDeletionService.Delete(request.Id);
        }
    }
}