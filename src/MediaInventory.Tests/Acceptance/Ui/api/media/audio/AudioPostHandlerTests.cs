using System;
using System.Net;
using MediaInventory.Core.Media;
using MediaInventory.Tests.Acceptance.Common.Http;
using MediaInventory.Tests.Common.Data.TestData;
using MediaInventory.UI.api.media.audio;
using NUnit.Framework;
using Should;

namespace MediaInventory.Tests.Acceptance.Ui.api.media.audio
{
    [TestFixture]
    public class AudioPostHandlerTests
    {
        private const string PostAudioUrl = "api/media/audio";

        private const string ArtistName = "Rush";
        private const string Title = "2112";
        private const MediaFormat MediaFormat = Core.Media.MediaFormat.Vinyl;
        private readonly DateTime Released = DateTime.Parse("4/1/1976");
        private readonly DateTime Purchased = DateTime.Parse("4/1/1990");
        private const decimal PurchasePrice = 8.95M;
        private const string PurchaseLocation = "amazon.com";
        private const int MediaCount = 1;
        private const string Notes = "Notes about 2112";

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
            var artist = _testData.Artists.Create(x => x.Name = ArtistName).Artist;

            var request = new AudioPostHandler.Request
            {
                ArtistName = ArtistName,
                Title = Title,
                MediaFormat = MediaFormat,
                Released = Released,
                Purchased = Purchased,
                PurchasePrice = PurchasePrice,
                PurchaseLocation = PurchaseLocation,
                MediaCount = MediaCount,
                Notes = Notes
            };

            var response = Http.ForUi.AsPublic(false, false, PostAudioUrl)
                .PostJson<AudioPostHandler.Request, AudioModel>(request);

            _testData.IncludeInCleanUp(_testData.Repositories.Audios.Get(response.Data.Id));

            response.Status.ShouldEqual(HttpStatusCode.OK);
            response.Data.Id.ShouldNotEqual(Guid.Empty);
            response.Data.ArtistName.ShouldEqual(ArtistName);
            response.Data.ArtistId.ShouldEqual(artist.Id);
            response.Data.Title.ShouldEqual(Title);
            response.Data.MediaFormat.ShouldEqual(MediaFormat);
            response.Data.Released.ShouldEqual(Released);
            response.Data.Purchased.ShouldEqual(Purchased);
            response.Data.PurchasePrice.ShouldEqual(PurchasePrice);
            response.Data.PurchaseLocation.ShouldEqual(PurchaseLocation);
            response.Data.MediaCount.ShouldEqual(MediaCount);
            response.Data.Notes.ShouldEqual(Notes);
        }
    }
}