using System.Linq;
using MediaInventory.Core.Artist;
using MediaInventory.Tests.Common.Fakes.Data;
using NUnit.Framework;
using Should;

namespace MediaInventory.Tests.Unit.Core.Artist
{
    [TestFixture]
    public class ArtistDeletionServiceTests
    {
        private MemoryRepository<MediaInventory.Core.Artist.Artist> _artists;
        private ArtistDeletionService _artistDeletionService;

        [SetUp]
        public void SetUp()
        {
            _artists = new MemoryRepository<MediaInventory.Core.Artist.Artist>(x => x.Id);
            _artistDeletionService = new ArtistDeletionService(_artists);
        }

        [Test]
        public void should_delete_artist()
        {
            var artist = _artists.Add(new MediaInventory.Core.Artist.Artist());
            _artists.Count(x => x.Id == artist.Id).ShouldEqual(1);

            _artistDeletionService.Delete(artist.Id);

            _artists.Count(x => x.Id == artist.Id).ShouldEqual(0);
        }
    }
}