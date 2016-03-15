using System;
using MediaInventory.Core.Artist;
using MediaInventory.Infrastructure.Common.Exceptions;
using MediaInventory.Tests.Common.Fakes.Data;
using NUnit.Framework;
using Should;

namespace MediaInventory.Tests.Unit.Core.Artist
{
    [TestFixture]
    public class ArtistModificationServiceTests
    {
        private const string OldName = "Rash";
        private const string NewName = "Rush";

        private MemoryRepository<MediaInventory.Core.Artist.Artist> _artists;
        private ArtistModificationService _artistModificationService;

        [SetUp]
        public void SetUp()
        {
            _artists = new MemoryRepository<MediaInventory.Core.Artist.Artist>(x => x.Id);
            _artistModificationService = new ArtistModificationService(_artists,
                new ArtistValidator(_artists));
        }

        [Test]
        public void should_modify_artist()
        {
            var artist = _artists.Add(new MediaInventory.Core.Artist.Artist { Name = OldName });
            _artistModificationService.Modify(artist.Id, x => x.Name = NewName);

            artist.Name.ShouldEqual(NewName);
        }

        [Test]
        public void should_throw_not_found_when_artist_does_not_exist()
        {
            Assert.Throws<NotFoundException>(() => _artistModificationService.Modify(Guid.NewGuid(), null));
        }
    }
}