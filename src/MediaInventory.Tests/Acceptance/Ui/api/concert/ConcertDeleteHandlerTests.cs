using System.Linq;
using System.Net;
using MediaInventory.Tests.Acceptance.Common.Http;
using MediaInventory.Tests.Common.Data.TestData;
using NUnit.Framework;
using Should;

namespace MediaInventory.Tests.Acceptance.Ui.api.concert
{
    [TestFixture]
    public class ConcertDeleteHandlerTests
    {
        private const string DeleteConcertUrl = "api/concert/{0}";

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
        public void should_delete_concert()
        {
            var concert = _testData.Concerts.Create().Concert;

            var response = Http.ForUi.AsPublic(false, false, DeleteConcertUrl, concert.Id).Delete();

            response.Status.ShouldEqual(HttpStatusCode.OK);
            _testData.Repositories.ConcertRepository.Count(x => x.Id == concert.Id).ShouldEqual(0);

            _testData.ExcludeInCleanup(concert);
        }
    }
}