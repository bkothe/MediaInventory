using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using MediaInventory.Tests.Acceptance.Common.Http;
using MediaInventory.Tests.Common.Data.TestData;
using MediaInventory.Tests.Common.Extensions;
using MediaInventory.UI.api.artist;
using NUnit.Framework;
using Should;

namespace MediaInventory.Tests.Acceptance.Ui.api.artist
{
    [TestFixture]
    public class ArtistGetHandlerTests
    {
        private const string GetArtistUrl = "api/artist/{0}";
        private const string EnumerateArtistsUrl = "api/artist";

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
        public void should_get_artist()
        {
            _testData.Artists.Create(x => x.Name = "Rush");

            var response = Http.ForUi.AsPublic(false, false, GetArtistUrl, _testData.Tracking.Artists.Single().Id).GetJson<ArtistModel>();

            response.Status.ShouldEqual(HttpStatusCode.OK);
            response.Data.Id.ShouldEqual(_testData.Tracking.Artists.Single().Id);
            response.Data.Name.ShouldEqual("Rush");
        }

        [Test]
        public void should_enumerate_artists()
        {
            _testData.Artists.Create(x => x.Name = "Rush");
            _testData.Artists.Create(x => x.Name = "Dream Theater");

            var response = Http.ForUi.AsPublic(false, false, EnumerateArtistsUrl).GetJson<List<ArtistModel>>();

            response.Status.ShouldEqual(HttpStatusCode.OK);
            response.Data.Count.ShouldEqual(2);
            response.Data.Select(x => x.Name).ShouldBeEquivalent(new [] { "Rush", "Dream Theater" });
        }

        [Test]
        public void should_return_not_found()
        {
            Http.ForUi.AsPublic(false, false, GetArtistUrl, Guid.NewGuid())
                .GetJson<ArtistModel>().Status.ShouldEqual(HttpStatusCode.NotFound);
        }
    }
}