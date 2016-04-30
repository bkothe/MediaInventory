using System;
using System.Net;
using MediaInventory.Tests.Acceptance.Common.Http;
using MediaInventory.Tests.Common.Data.TestData;
using MediaInventory.UI.api.concert;
using NUnit.Framework;
using Should;

namespace MediaInventory.Tests.Acceptance.Ui.api.concert
{
    [TestFixture]
    public class ConcertPostHandlerTests
    {
        private const string PostConcertUrl = "api/concert";

        private const string ArtistName = "Rush";
        private const string VenueName = "Alpine Valley";
        private readonly DateTime ConcertDate = DateTime.Parse("4/1/1976");

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
        public void should_create_concert()
        {
            var request = new ConcertPostHandler.Request
            {
                ArtistName = ArtistName,
                Date = ConcertDate,
                VenueName = VenueName
            };

            var response = Http.ForUi.AsPublic(false, false, PostConcertUrl)
                .PostJson<ConcertPostHandler.Request, ConcertModel>(request);

            _testData.IncludeInCleanUp(_testData.Repositories.Concerts.Get(response.Data.Id));

            response.Status.ShouldEqual(HttpStatusCode.OK);
            response.Data.Id.ShouldNotEqual(Guid.Empty);
            response.Data.ArtistName.ShouldEqual(ArtistName);
            response.Data.ArtistId.ShouldNotEqual(Guid.Empty);
            response.Data.Venue.Id.ShouldNotEqual(Guid.Empty);
            response.Data.Venue.Name.ShouldEqual(VenueName);
            response.Data.Venue.City.ShouldBeNull();
            response.Data.Venue.State.ShouldBeNull();
            response.Data.Date.ShouldEqual(ConcertDate);
        }
    }
}