using System;
using FluentValidation;
using MediaInventory.Infrastructure.Common.Collections;
using MediaInventory.Infrastructure.Common.Data.Orm;

namespace MediaInventory.Core.Artist
{
    public interface IArtistModificationService
    {
        Artist Modify(Guid id, Action<Artist> modify);
    }

    public class ArtistModificationService : IArtistModificationService
    {
        private readonly IRepository<Artist> _artists;
        private readonly ArtistValidator _artistValidator;

        public ArtistModificationService(IRepository<Artist> artists, ArtistValidator artistValidator)
        {
            _artists = artists;
            _artistValidator = artistValidator;
        }

        public Artist Modify(Guid id, Action<Artist> modify)
        {
            var artist = _artists.FirstOrThrowNotFound(x => x.Id == id, id);

            TryModify(artist, modify);

            modify(artist);

            return artist;
        }

        private void TryModify(Artist originalArtist, Action<Artist> modify)
        {
            var artist = originalArtist.Clone();

            modify(artist);

            _artistValidator.ValidateAndThrow(artist);
        }
    }
}