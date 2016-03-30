using System;
using System.Net;
using MediaInventory.Tests.Acceptance.Common.Http;
using MediaInventory.Tests.Common.Data.TestData;
using MediaInventory.UI.api.venue;
using NUnit.Framework;
using Should;

namespace MediaInventory.Tests.Acceptance.Ui.api.venue
{
    [TestFixture]
    public class VenuePostHandlerTests
    {
        private const string PostVenueUrl = "api/venue";

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
            const string name = "Alpine Valley";
            const string city = "East Troy";
            const string state = "IL";

            var request = new VenuePostHandler.Request { Name = name, City = city, State = state };
            var response = Http.ForUi.AsPublic(false, false, PostVenueUrl)
                .PostJson<VenuePostHandler.Request, VenueModel>(request);

            _testData.IncludeInCleanUp(_testData.Repositories.Venues.Get(response.Data.Id));

            response.Status.ShouldEqual(HttpStatusCode.OK);
            response.Data.Id.ShouldNotEqual(Guid.Empty);
            response.Data.Name.ShouldEqual(name);
            response.Data.City.ShouldEqual(city);
            response.Data.State.ShouldEqual(state);
        }
    }
}