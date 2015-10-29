using System;
using MediaInventory.Infrastructure.Framework.Data.Orm;

namespace MediaInventory.Core.Artist
{
    public interface IArtistDeletionService
    {
        void Delete(Guid artistId);
    }

    public class ArtistDeletionService : IArtistDeletionService
    {
        private readonly IRepository<Artist> _artists;

        public ArtistDeletionService(IRepository<Artist> artists)
        {
            _artists = artists;
        }

        public void Delete(Guid artistId)
        {
            _artists.Delete(artistId);
        }
    }
}