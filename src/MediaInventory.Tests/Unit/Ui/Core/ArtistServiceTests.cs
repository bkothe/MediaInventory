using MediaInventory.Core.Artist;
using MediaInventory.Tests.Common.Extensions;
using MediaInventory.Tests.Common.Fakes.Data;
using MediaInventory.UI.Core;
using NSubstitute;
using NUnit.Framework;
using Ploeh.AutoFixture.NUnit2;
using Should;

namespace MediaInventory.Tests.Unit.Ui.Core
{
    [TestFixture]
    public class ArtistServiceTests
    {
        private MemoryRepository<Artist> _artists;
        private IArtistCreationService _artistCreationService;
        private ArtistService _artistService;

        [SetUp]
        public void SetUp()
        {
            _artists = new MemoryRepository<Artist>(x => x.Id);
            _artistCreationService = Substitute.For<IArtistCreationService>();
            _artistService = new ArtistService(_artists, _artistCreationService);
        }

        [Test]
        public void should_not_create_artist_when_artist_with_specified_name_exists()
        {
            _artistService.GetOrCreateArtist(_artists.Add(new Artist { Name = RandomString.GenerateAlphaNumeric() }).Name);

            _artistCreationService.DidNotReceive().Create(Arg.Any<string>());
        }

        [Test, AutoData]
        public void should_return_existing_artist(Artist autoArtist)
        {
            var result = _artistService.GetOrCreateArtist(_artists.Add(autoArtist).Name);

            result.Id.ShouldEqual(autoArtist.Id);
            result.Name.ShouldEqual(autoArtist.Name);
        }

        [Test, AutoData]
        public void should_create_artist_when_artist_with_specified_name_does_not_exist(Artist autoArtist)
        {
            _artistCreationService.Create(Arg.Do<string>(x => _artists.Add(autoArtist))).Returns(autoArtist);

            var result = _artistService.GetOrCreateArtist(autoArtist.Name);

            _artistCreationService.Received().Create(Arg.Is(autoArtist.Name));

            result.Id.ShouldEqual(autoArtist.Id);
            result.Name.ShouldEqual(autoArtist.Name);
        }
    }
}