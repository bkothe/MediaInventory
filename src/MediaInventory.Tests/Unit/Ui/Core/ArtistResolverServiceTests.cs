using MediaInventory.Core.Artist;
using MediaInventory.Tests.Common.Extensions;
using MediaInventory.Tests.Common.Fakes.Data;
using MediaInventory.UI.Core;
using NSubstitute;
using NUnit.Framework;
using Ploeh.AutoFixture.NUnit3;
using Should;

namespace MediaInventory.Tests.Unit.Ui.Core
{
    [TestFixture]
    public class ArtistResolverServiceTests
    {
        private MemoryRepository<Artist> _artists;
        private IArtistCreationService _artistCreationService;
        private ArtistResolverService _sut;

        [SetUp]
        public void SetUp()
        {
            _artists = new MemoryRepository<Artist>(x => x.Id);
            _artistCreationService = Substitute.For<IArtistCreationService>();
            _sut = new ArtistResolverService(_artists, _artistCreationService);
        }

        [Test, AutoData]
        public void should_return_existing_artist(Artist autoArtist)
        {
            var result = _sut.ResolveArtist(_artists.Add(autoArtist).Name);

            result.Id.ShouldEqual(autoArtist.Id);
            result.Name.ShouldEqual(autoArtist.Name);
        }

        [Test]
        public void should_not_call_create_when_artist_with_specified_name_exists()
        {
            _sut.ResolveArtist(_artists.Add(new Artist { Name = RandomString.GenerateAlphaNumeric() }).Name);

            _artistCreationService.DidNotReceiveWithAnyArgs().Create(Arg.Any<string>());
        }

        [Test, AutoData]
        public void should_call_create_when_artist_with_specified_name_does_not_exist(Artist autoArtist)
        {
            _sut.ResolveArtist(autoArtist.Name);

            _artistCreationService.Received(1).Create(Arg.Is(autoArtist.Name));
        }
    }
}