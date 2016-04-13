using System.Linq;
using MediaInventory.Core.Artist;
using MediaInventory.Infrastructure.Common.Data.Orm;

namespace MediaInventory.UI.Core
{
    public interface IArtistService
    {
        Artist GetOrCreateArtist(string artistName);
    }

    public class ArtistService : IArtistService
    {
        private readonly IRepository<Artist> _artists;
        private readonly IArtistCreationService _artistCreationService;

        public ArtistService(IRepository<Artist> artists, IArtistCreationService artistCreationService)
        {
            _artists = artists;
            _artistCreationService = artistCreationService;
        }

        public Artist GetOrCreateArtist(string artistName)
        {
            return _artists.FirstOrDefault(x => x.Name == artistName) ??
                _artistCreationService.Create(artistName);
        }
    }
}