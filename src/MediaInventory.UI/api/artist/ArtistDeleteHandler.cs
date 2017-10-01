namespace MediaInventory.Ui.api.artist
{
    public class ArtistDeleteHandler
    {
        private readonly ArtistDeletionService _artistDeletionService;

        public ArtistDeleteHandler(ArtistDeletionService artistDeletionService)
        {
            _artistDeletionService = artistDeletionService;
        }

        public void Execute_Id(RequestGuidId request)
        {
            _artistDeletionService.Delete(request.Id);
        }
    }
}