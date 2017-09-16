using System;
using FluentAssertions;
using MediaInventory.Core.Artist;
using MediaInventory.Infrastructure.Common.Exceptions;
using MediaInventory.Tests.Common.Fakes.Data;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace MediaInventory.Tests.Unit.Core.Artist
{
    [TestFixture]
    public class ArtistModificationServiceTests
    {
        private IFixture _fixture;
        private MemoryRepository<MediaInventory.Core.Artist.Artist> _artists;
        private ArtistModificationService _sut;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _fixture = new Fixture();
        }

        [SetUp]
        public void SetUp()
        {
            _artists = new MemoryRepository<MediaInventory.Core.Artist.Artist>(x => x.Id);
            _sut = new ArtistModificationService(_artists, new ArtistValidator(_artists));
        }

        [Test]
        public void should_modify_artist()
        {
            var expectedName = _fixture.Create<string>();
            var artist = _artists.Add(new MediaInventory.Core.Artist.Artist { Name = _fixture.Create<string>() });

            _sut.Modify(artist.Id, x => x.Name = expectedName).Name.Should().Be(expectedName);
        }

        [Test]
        public void should_throw_not_found_when_artist_does_not_exist()
        {
            Action action = () => _sut.Modify(Guid.NewGuid(), null);

            action.ShouldThrow<NotFoundException>();
        }
    }
}