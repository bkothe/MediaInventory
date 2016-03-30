using System.Linq;
using System.Net;
using MediaInventory.Tests.Acceptance.Common.Http;
using MediaInventory.Tests.Common.Data.TestData;
using NUnit.Framework;
using Should;

namespace MediaInventory.Tests.Acceptance.Ui.api.media.audio
{
    [TestFixture]
    public class AudioDeleteHandlerTests
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
        public void should_delete_audio()
        {
            var audio = _testData.Audios.Create().Audio;

            var response = Http.ForUi.AsPublic(false, false, DeleteAudioUrl, audio.Id).Delete();

            response.Status.ShouldEqual(HttpStatusCode.OK);
            _testData.Repositories.Audios.Count(x => x.Id == audio.Id).ShouldEqual(0);

            _testData.ExcludeInCleanup(audio);
        }
    }
}