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
    public class ArtistCreationServiceTests
    {
        private IFixture _fixture;
        private MemoryRepository<MediaInventory.Core.Artist.Artist> _artists; 
        private ArtistValidator _artistValidator;
        private ArtistCreationService _sut;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _fixture = new Fixture();
        }

        [SetUp]
        public void SetUp()
        {
            _artists = new MemoryRepository<MediaInventory.Core.Artist.Artist>(x => x.Id);
            _artistValidator = new ArtistValidator(_artists);
            _sut = new ArtistCreationService(_artists, _artistValidator);
        }

        [Test]
        public void should_add_artist_to_repository()
        {
            var artist = _sut.Create(_fixture.Create<string>());

            _artists.Should().ContainSingle(x => x == artist);
        }

        [Test]
        public void should_create_artist()
        {
            var expectedArtistName = new Fixture().Create<string>();

            _sut.Create(expectedArtistName).Name.Should().Be(expectedArtistName);
        }

        [Test]
        public void should_throw_exception_when_artist_name_is_empty()
        {
            Action action = () => _sut.Create("");

            action.ShouldThrow<ValidationException>();
        }
    }
}