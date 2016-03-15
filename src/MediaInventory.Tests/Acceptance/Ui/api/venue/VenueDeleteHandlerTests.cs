using System.Linq;
using System.Net;
using MediaInventory.Tests.Acceptance.Common.Http;
using MediaInventory.Tests.Common.Data.TestData;
using NUnit.Framework;
using Should;

namespace MediaInventory.Tests.Acceptance.Ui.api.venue
{
    [TestFixture]
    public class VenueDeleteHandlerTests
    {
        private const string DeleteVenueUrl = "api/venue/{0}";

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
        public void should_delete_venue()
        {
            var venue = _testData.Venues.Create().Venue;

            var response = Http.ForUi.AsPublic(false, false, DeleteVenueUrl, venue.Id).Delete();

            response.Status.ShouldEqual(HttpStatusCode.OK);
            _testData.Repositories.VenueRepository.Count(x => x.Id == venue.Id).ShouldEqual(0);

            _testData.ExcludeInCleanup(venue);
        }
    }
}