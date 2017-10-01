namespace MediaInventory.Ui.Core
{
    public interface IArtistResolverService
    {
        Artist ResolveArtist(string artistName);
    }

    public class ArtistResolverService : IArtistResolverService
    {
        private readonly IRepository<Artist> _artists;
        private readonly IArtistCreationService _artistCreationService;

        public ArtistResolverService(IRepository<Artist> artists, IArtistCreationService artistCreationService)
        {
            _artists = artists;
            _artistCreationService = artistCreationService;
        }

        public Artist ResolveArtist(string artistName)
        {
            return _artists.FirstOrDefault(x => x.Name == artistName) ??
                _artistCreationService.Create(artistName);
        }
    }
}