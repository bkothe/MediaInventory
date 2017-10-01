namespace MediaInventory.Ui.api.venue
{
    public class VenueDeleteHandler
    {
        private readonly VenueDeletionService _venueDeletionService;

        public VenueDeleteHandler(VenueDeletionService venueDeletionService)
        {
            _venueDeletionService = venueDeletionService;
        }

        public void Execute_Id(RequestGuidId request)
        {
            _venueDeletionService.Delete(request.Id);
        }
    }
}