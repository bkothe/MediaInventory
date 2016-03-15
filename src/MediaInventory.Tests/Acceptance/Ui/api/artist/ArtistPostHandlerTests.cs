using System;
using System.Net;
using MediaInventory.Tests.Acceptance.Common.Http;
using MediaInventory.Tests.Common.Data.TestData;
using MediaInventory.UI.api.artist;
using NUnit.Framework;
using Should;

namespace MediaInventory.Tests.Acceptance.Ui.api.artist
{
    public class ArtistPostHandlerTests
    {
        private const string PostArtistUrl = "api/artist";

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
        public void should_create_artist()
        {
            var request = new ArtistPostHandler.Request { Name = "Rush" };
            var response = Http.ForUi.AsPublic(false, false, PostArtistUrl)
                .PostJson<ArtistPostHandler.Request, ArtistModel>(request);

            response.Status.ShouldEqual(HttpStatusCode.OK);
            response.Data.Id.ShouldNotEqual(Guid.Empty);
            response.Data.Name.ShouldEqual(request.Name);

            _testData.IncludeInCleanUp(_testData.Repositories.ArtistRepository.Get(response.Data.Id));
        }
    }
}