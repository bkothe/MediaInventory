using MediaInventory.Infrastructure.Common.Data.Orm;
using MediaInventory.Infrastructure.Common.Exceptions;

namespace MediaInventory.Core.Artist
{
    public interface IArtistCreationService
    {
        Artist Create(string name);
    }

    public class ArtistCreationService : IArtistCreationService
    {
        private readonly IRepository<Artist> _artists;
        private readonly ArtistValidator _artistValidator;

        public ArtistCreationService(IRepository<Artist> artists, ArtistValidator artistValidator)
        {
            _artists = artists;
            _artistValidator = artistValidator;
        }

        public Artist Create(string name)
        {
            var artist = new Artist { Name = name };

            _artistValidator.ValidateAndThrow(artist);

            return _artists.Add(artist);
        }
    }
}