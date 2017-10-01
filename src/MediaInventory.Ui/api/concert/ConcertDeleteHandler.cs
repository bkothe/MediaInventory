namespace MediaInventory.Ui.api.concert
{
    public class ConcertDeleteHandler
    {
        private readonly ConcertDeletionService _concertDeletionService;

        public ConcertDeleteHandler(ConcertDeletionService concertDeletionService)
        {
            _concertDeletionService = concertDeletionService;
        }

        public void Execute_Id(RequestGuidId request)
        {
            _concertDeletionService.Delete(request.Id);
        }
    }
}