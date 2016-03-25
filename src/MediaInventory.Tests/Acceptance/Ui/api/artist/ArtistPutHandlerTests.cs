using System.Net;
using MediaInventory.Tests.Acceptance.Common.Http;
using MediaInventory.Tests.Common.Data.TestData;
using MediaInventory.UI.api.artist;
using NUnit.Framework;
using Should;

namespace MediaInventory.Tests.Acceptance.Ui.api.artist
{
    [TestFixture]
    public class ArtistPutHandlerTests
    {
        private const string PutArtistUrl = "api/artist/{0}";

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
        public void should_modify_artist()
        {
            const string nameModified = "Rush";
            var artist = _testData.Artists.Create().Artist;
            
            Http.ForUi.AsPublic(false, false, PutArtistUrl, artist.Id)
                .PutJson(new ArtistPutHandler.Request { Name = nameModified })
                .Status.ShouldEqual(HttpStatusCode.OK);

            _testData.Session.Refresh(artist);

            artist.Name.ShouldEqual(nameModified);
        }
    }
}