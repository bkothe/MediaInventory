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
    public class ConcertPutHandlerTests
    {
        private const string PutConcertUrl = "api/concert/{0}";

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
        public void should_modify_concert()
        {
            var artistModified = _testData.Artists.Create().Artist;
            var venueModified = _testData.Venues.Create().Venue;
            var dateModified = DateTime.Now.AddDays(4);
            var concert = _testData.Concerts.Create().Concert;

            Http.ForUi.AsPublic(false, false, PutConcertUrl, concert.Id)
                .PutJson(new ConcertPutHandler.Request
                {
                    ArtistId = artistModified.Id,
                    VenueId = venueModified.Id,
                    Date = dateModified
                })
                .Status.ShouldEqual(HttpStatusCode.OK);

            _testData.Session.Refresh(concert);

            concert.Artist.Id.ShouldEqual(artistModified.Id);
            concert.Venue.Id.ShouldEqual(venueModified.Id);
            concert.Date.ShouldBeWithinSeconds(dateModified, 1);
        }

        [Test]
        public void should_return_not_found_when_artist_does_not_exist()
        {
            Http.ForUi.AsPublic(false, false, PutConcertUrl, _testData.Concerts.Create().Concert.Id)
                .PutJson(new ConcertPutHandler.Request
                {
                    ArtistId = Guid.NewGuid(),
                    VenueId = _testData.Venues.Create().Venue.Id,
                    Date = DateTime.Now
                })
                .Status.ShouldEqual(HttpStatusCode.NotFound);
        }

        [Test]
        public void should_return_not_found_when_venue_does_not_exist()
        {
            Http.ForUi.AsPublic(false, false, PutConcertUrl, _testData.Concerts.Create().Concert.Id)
                .PutJson(new ConcertPutHandler.Request
                {
                    ArtistId = _testData.Artists.Create().Artist.Id,
                    VenueId = Guid.NewGuid(),
                    Date = DateTime.Now
                })
                .Status.ShouldEqual(HttpStatusCode.NotFound);
        }
    }
}