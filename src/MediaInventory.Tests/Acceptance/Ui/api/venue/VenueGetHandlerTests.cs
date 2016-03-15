using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using MediaInventory.Tests.Acceptance.Common.Http;
using MediaInventory.Tests.Common.Data.TestData;
using MediaInventory.Tests.Common.Extensions;
using MediaInventory.UI.api.venue;
using NUnit.Framework;
using Should;

namespace MediaInventory.Tests.Acceptance.Ui.api.venue
{
    [TestFixture]
    public class VenueGetHandlerTests
    {
        private const string GetVenueUrl = "api/venue/{0}";
        private const string EnumerateVenuesUrl = "api/venue";

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
        public void should_get_venue()
        {
            const string name = "Alpine Valley";
            const string city = "East Troy";
            const string state = "WI";

            _testData.Venues.Create(x =>
            {
                x.Name = name;
                x.City = city;
                x.State = state;
            });

            var response = Http.ForUi.AsPublic(false, false, GetVenueUrl,
                _testData.Tracking.Venues.Single().Id).GetJson<VenueModel>();

            response.Status.ShouldEqual(HttpStatusCode.OK);
            response.Data.Id.ShouldEqual(_testData.Tracking.Venues.Single().Id);
            response.Data.Name.ShouldEqual(name);
            response.Data.City.ShouldEqual(city);
            response.Data.State.ShouldEqual(state);
        }

        [Test]
        public void should_enumerate_venues()
        {
            const string name1 = "Alpine Valley";
            const string name2 = "Red Rocks";
            _testData.Venues.Create(x => x.Name = name1);
            _testData.Venues.Create(x => x.Name = name2);

            var response = Http.ForUi.AsPublic(false, false, EnumerateVenuesUrl)
                .GetJson<List<VenueModel>>();

            response.Status.ShouldEqual(HttpStatusCode.OK);
            response.Data.Count.ShouldEqual(2);
            response.Data.Select(x => x.Name).ShouldBeEquivalent(new[] { name1, name2 });
        }

        [Test]
        public void should_return_not_found()
        {
            Http.ForUi.AsPublic(false, false, GetVenueUrl, Guid.NewGuid())
                .GetJson<VenueModel>().Status.ShouldEqual(HttpStatusCode.NotFound);
        }
    }
}