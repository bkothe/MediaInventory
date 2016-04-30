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
    public class ArtistResolverServiceTests
    {
        private MemoryRepository<Artist> _artists;
        private IArtistCreationService _artistCreationService;
        private ArtistResolverService _artistResolverService;

        [SetUp]
        public void SetUp()
        {
            _artists = new MemoryRepository<Artist>(x => x.Id);
            _artistCreationService = Substitute.For<IArtistCreationService>();
            _artistResolverService = new ArtistResolverService(_artists, _artistCreationService);
        }

        [Test, AutoData]
        public void should_return_existing_artist(Artist autoArtist)
        {
            var result = _artistResolverService.ResolveArtist(_artists.Add(autoArtist).Name);

            result.Id.ShouldEqual(autoArtist.Id);
            result.Name.ShouldEqual(autoArtist.Name);
        }

        [Test]
        public void should_not_call_create_when_artist_with_specified_name_exists()
        {
            _artistResolverService.ResolveArtist(_artists.Add(new Artist { Name = RandomString.GenerateAlphaNumeric() }).Name);

            _artistCreationService.DidNotReceiveWithAnyArgs().Create(Arg.Any<string>());
        }

        [Test]
        public void should_call_create_when_artist_with_specified_name_does_not_exist(Artist autoArtist)
        {
            _artistResolverService.ResolveArtist(autoArtist.Name);

            _artistCreationService.Received(1).Create(autoArtist.Name);
        }
    }
}