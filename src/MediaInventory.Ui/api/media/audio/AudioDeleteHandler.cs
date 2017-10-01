namespace MediaInventory.Ui.api.media.audio
{
    public class AudioDeleteHandler
    {
        private readonly AudioDeletionService _audioDeletionService;

        public AudioDeleteHandler(AudioDeletionService audioDeletionService)
        {
            _audioDeletionService = audioDeletionService;
        }

        public void Execute_Id(RequestGuidId request)
        {
            _audioDeletionService.Delete(request.Id);
        }
    }
}