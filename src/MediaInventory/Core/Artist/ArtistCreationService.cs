using FluentValidation;
using MediaInventory.Infrastructure.Framework.Data.Orm;

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

            _artists.Add(artist);

            return artist;
        }
    }
}