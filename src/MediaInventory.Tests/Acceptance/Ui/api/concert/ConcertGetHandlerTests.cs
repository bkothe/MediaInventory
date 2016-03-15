using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ConcertGetHandlerTests
    {
        private const string GetConcertUrl = "api/concert/{0}";
        private const string EnumerateConcertsUrl = "api/concert";

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
        public void should_enumerate_concerts()
        {
            var concertDate1 = _testData.Concerts.Create().Concert.Date;
            var concertDate2 = _testData.Concerts.Create().Concert.Date;

            var response = Http.ForUi.AsPublic(false, false, EnumerateConcertsUrl).GetJson<List<ConcertEnumerationModel>>();

            response.Status.ShouldEqual(HttpStatusCode.OK);
            response.Data.Count.ShouldEqual(2);
            response.Data.Select(x => x.Date.Ticks).ShouldBeEquivalent(new[] { concertDate1.Ticks, concertDate2.Ticks });
        }

        [Test]
        public void should_get_concert()
        {
            var concert = _testData.Concerts.Create().Concert;

            var response = Http.ForUi.AsPublic(false, false, GetConcertUrl, concert.Id).GetJson<ConcertModel>();

            response.Status.ShouldEqual(HttpStatusCode.OK);
            response.Data.Id.ShouldEqual(concert.Id);
            response.Data.Date.ShouldEqual(concert.Date);
            response.Data.Artist.Id.ShouldEqual(concert.Artist.Id);
            response.Data.Artist.Name.ShouldEqual(concert.Artist.Name);
            response.Data.Venue.Id.ShouldEqual(concert.Venue.Id);
            response.Data.Venue.Name.ShouldEqual(concert.Venue.Name);
            response.Data.Venue.City.ShouldEqual(concert.Venue.City);
            response.Data.Venue.State.ShouldEqual(concert.Venue.State);
        }

        [Test]
        public void should_return_not_found()
        {
            Http.ForUi.AsPublic(false, false, GetConcertUrl, Guid.NewGuid())
                .GetJson<ConcertModel>().Status.ShouldEqual(HttpStatusCode.NotFound);
        }
    }
}