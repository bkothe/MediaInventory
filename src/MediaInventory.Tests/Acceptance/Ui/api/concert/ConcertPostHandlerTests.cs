using System;
using System.Net;
using MediaInventory.Tests.Acceptance.Common.Http;
using MediaInventory.Tests.Common.Data.TestData;
using MediaInventory.Tests.Common.Extensions;
using MediaInventory.UI.api.concert;
using NUnit.Framework;
using Should;

namespace MediaInventory.Tests.Acceptance.Ui.api.concert
{
    [TestFixture]
    public class ConcertPostHandlerTests
    {
        private const string PostConcertUrl = "api/concert";

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
        public void should_create_audio()
        {
            DateTime concertDate = DateTime.Now.AddDays(-10);
            var artist = _testData.Artists.Create().Artist;
            var venue = _testData.Venues.Create().Venue;

            var request = new ConcertPostHandler.Request
            {
                ArtistId = artist.Id,
                Date = concertDate,
                VenueId = venue.Id
            };

            var response = Http.ForUi.AsPublic(false, false, PostConcertUrl)
                .PostJson<ConcertPostHandler.Request, ConcertModel>(request);

            _testData.IncludeInCleanUp(_testData.Repositories.Concerts.Get(response.Data.Id));

            response.Status.ShouldEqual(HttpStatusCode.OK);
            response.Data.Id.ShouldNotEqual(Guid.Empty);
            response.Data.ArtistName.ShouldEqual(artist.Name);
            response.Data.ArtistId.ShouldEqual(artist.Id);
            response.Data.Venue.Id.ShouldEqual(venue.Id);
            response.Data.Venue.Name.ShouldEqual(venue.Name);
            response.Data.Venue.City.ShouldEqual(venue.City);
            response.Data.Venue.State.ShouldEqual(venue.State);
            response.Data.Date.ShouldBeWithinSeconds(concertDate, 1);
        }
    }
}