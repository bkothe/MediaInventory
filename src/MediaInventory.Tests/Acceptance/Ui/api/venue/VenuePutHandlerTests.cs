using System.Net;
using MediaInventory.Tests.Acceptance.Common.Http;
using MediaInventory.Tests.Common.Data.TestData;
using MediaInventory.UI.api.venue;
using NUnit.Framework;
using Should;

namespace MediaInventory.Tests.Acceptance.Ui.api.venue
{
    [TestFixture]
    public class VenuePutHandlerTests
    {
        private const string PutVenueUrl = "api/venue/{0}";

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
        public void should_modify_venue()
        {
            const string nameModified = "Alpine Valley";
            const string cityModified = "East Troy";
            const string stateModified = "WI";
            var venue = _testData.Venues.Create().Venue;

            Http.ForUi.AsPublic(false, false, PutVenueUrl, venue.Id)
                .PutJson(new VenuePutHandler.Request { Name = nameModified, City = cityModified, State = stateModified })
                .Status.ShouldEqual(HttpStatusCode.OK);

            _testData.Session.Refresh(venue);

            venue.Name.ShouldEqual(nameModified);
            venue.City.ShouldEqual(cityModified);
            venue.State.ShouldEqual(stateModified);
        }
    }
}