using FluentValidation;
using MediaInventory.Core.Artist;
using MediaInventory.Infrastructure.Framework.Data.Orm;
using MediaInventory.Tests.Common.Fakes.Data;
using NSubstitute;
using NUnit.Framework;
using Should;

namespace MediaInventory.Tests.Unit.Core.Artist
{
    [TestFixture]
    public class ArtistCreationServiceTests
    {
        private const string ArtistName = "Rush";

        private MemoryRepository<MediaInventory.Core.Artist.Artist> _artists; 
        private ArtistCreationService _artistCreationService;
        private ArtistValidator _artistValidator;

        [SetUp]
        public void SetUp()
        {
            _artists = new MemoryRepository<MediaInventory.Core.Artist.Artist>(x => x.Id);
            _artistValidator = new ArtistValidator(_artists);
            _artistCreationService = new ArtistCreationService(_artists, _artistValidator);
        }

        [Test]
        public void should_add_artist_to_repository()
        {
            var artists = Substitute.For<IRepository<MediaInventory.Core.Artist.Artist>>();
            
            var artistCreationService = new ArtistCreationService(artists, _artistValidator);

            var artist = artistCreationService.Create(ArtistName);

            artists.Received(1).Add(Arg.Is(artist));
        }

        [Test]
        public void should_create_artist()
        {
            var artist = _artistCreationService.Create(ArtistName);

            artist.Name.ShouldEqual(ArtistName);
        }

        [Test, ExpectedException(typeof(ValidationException))]
        public void should_throw_exception_when_artist_name_is_empty()
        {
            _artistCreationService.Create("");
        }
    }
}