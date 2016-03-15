using System.Linq;
using System.Net;
using MediaInventory.Tests.Acceptance.Common.Http;
using MediaInventory.Tests.Common.Data.TestData;
using NUnit.Framework;
using Should;

namespace MediaInventory.Tests.Acceptance.Ui.api.artist
{
    [TestFixture]
    public class ArtistDeleteHandlerTests
    {
        private const string DeleteArtistUrl = "api/artist/{0}";

        private UiAcceptanceTestData _testData;

        [SetUp]
        public void SetUp()
        {
            _testData = TestData.ForAcceptanceTests().OnUi();
        }

        [TearDown]
        public void TearDown()
        {
            _testData.CleanUp();
        }

        [Test]
        public void should_delete_artist()
        {
            var artist = _testData.Artists.Create().Artist;

            var response = Http.ForUi.AsPublic(false, false, DeleteArtistUrl, artist.Id).Delete();

            response.Status.ShouldEqual(HttpStatusCode.OK);
            _testData.Repositories.ArtistRepository.Count(x => x.Id == artist.Id).ShouldEqual(0);

            _testData.ExcludeInCleanup(artist);
        }
    }
}