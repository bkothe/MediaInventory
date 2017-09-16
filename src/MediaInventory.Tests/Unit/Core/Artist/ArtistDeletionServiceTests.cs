using FluentAssertions;
using MediaInventory.Core.Artist;
using MediaInventory.Tests.Common.Fakes.Data;
using NUnit.Framework;

namespace MediaInventory.Tests.Unit.Core.Artist
{
    [TestFixture]
    public class ArtistDeletionServiceTests
    {
        private MemoryRepository<MediaInventory.Core.Artist.Artist> _artists;
        private ArtistDeletionService _sut;

        [SetUp]
        public void SetUp()
        {
            _artists = new MemoryRepository<MediaInventory.Core.Artist.Artist>(x => x.Id);
            _sut = new ArtistDeletionService(_artists);
        }

        [Test]
        public void should_delete_artist()
        {
            var artist = _artists.Add(new MediaInventory.Core.Artist.Artist());
            _artists.Should().Contain(x => x == artist);

            _sut.Delete(artist.Id);

            _artists.Should().NotContain(artist);
        }
    }
}