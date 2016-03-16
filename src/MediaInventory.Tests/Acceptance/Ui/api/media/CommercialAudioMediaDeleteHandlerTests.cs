using System.Linq;
using System.Net;
using MediaInventory.Tests.Acceptance.Common.Http;
using MediaInventory.Tests.Common.Data.TestData;
using NUnit.Framework;
using Should;

namespace MediaInventory.Tests.Acceptance.Ui.api.media
{
    [TestFixture]
    public class CommercialAudioMediaDeleteHandlerTests
    {
        private const string DeleteAudioUrl = "api/media/audio/{0}";

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
            var audioMedia = _testData.CommercialAudioMediea.Create().CommercialAudioMedia;

            var response = Http.ForUi.AsPublic(false, false, DeleteAudioUrl, audioMedia.Id).Delete();

            response.Status.ShouldEqual(HttpStatusCode.OK);
            _testData.Repositories.CommercialAudioMediaRepository.Count(x => x.Id == audioMedia.Id).ShouldEqual(0);

            _testData.ExcludeInCleanup(audioMedia);
        }
    }
}