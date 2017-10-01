using System;

namespace MediaInventory.Ui.api.artist
{
    public class ArtistPutHandler
    {
        private readonly ArtistModificationService _artistModificationService;

        public class Request
        {
            public Guid ArtistId { get; set; }
            public string Name { get; set; }
        }

        public ArtistPutHandler(ArtistModificationService artistModificationService)
        {
            _artistModificationService = artistModificationService;
        }

        public void Execute_ArtistId(Request request)
        {
            _artistModificationService.Modify(request.ArtistId, x => x.Name = request.Name);
        }
    }
}
