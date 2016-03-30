using System;
using System.Net;
using MediaInventory.Core.Artist;
using MediaInventory.Core.Media;
using MediaInventory.Tests.Acceptance.Common.Http;
using MediaInventory.Tests.Common.Data.TestData;
using MediaInventory.UI.api.media.audio;
using NUnit.Framework;
using Should;

namespace MediaInventory.Tests.Acceptance.Ui.api.media.audio
{
    [TestFixture]
    public class AudioPutHandlerTests
    {
        private const string PutAudioUrl = "api/media/audio/{0}";

        private Artist ArtistModified;
        private const string TitleModified = "Six Degrees Of Inner Turbulence";
        private const MediaFormat MediaFormatModified = MediaFormat.Shn;
        private readonly DateTime ReleasedModified = DateTime.Now.AddYears(-10).Date;
        private readonly DateTime PurchasedModified = DateTime.Now.AddYears(-9).Date;
        private const decimal PurchasePriceModified = 15M;
        private const string PurchaseLocationModified = "Kiss The Sky";
        private const int MediaCountModified = 6;
        private const string NotesModified = "Another DT album.";

        private UiAcceptanceTestData _testData;

        [SetUp]
        public void SetUp()
        {
            _testData = TestData.ForAcceptanceTests().OnUi();
            ArtistModified = _testData.Artists.Create().Artist;
        }

        [TearDown]
        public void TearDown()
        {
            _testData.CleanUp();
        }

        [Test]
        public void should_modify_audio()
        {
            var audio = _testData.Audios.Create().Audio;

            Http.ForUi.AsPublic(false, false, PutAudioUrl, audio.Id)
                .PutJson(new AudioPutHandler.Request
                {
                    ArtistId = ArtistModified.Id,
                    Title = TitleModified,
                    MediaFormat = (int)MediaFormatModified,
                    Released = ReleasedModified,
                    Purchased = PurchasedModified,
                    PurchasePrice = PurchasePriceModified,
                    PurchaseLocation = PurchaseLocationModified,
                    MediaCount = MediaCountModified,
                    Notes = NotesModified
                })
                .Status.ShouldEqual(HttpStatusCode.OK);

            _testData.Session.Refresh(audio);

            audio.Artist.Id.ShouldEqual(ArtistModified.Id);
            audio.Title.ShouldEqual(TitleModified);
            audio.MediaFormat.ShouldEqual(MediaFormatModified);
            audio.Released.ShouldEqual(ReleasedModified);
            audio.Purchased.ShouldEqual(PurchasedModified);
            audio.PurchasePrice.ShouldEqual(PurchasePriceModified);
            audio.PurchaseLocation.ShouldEqual(PurchaseLocationModified);
            audio.MediaCount.ShouldEqual(MediaCountModified);
            audio.Notes.ShouldEqual(NotesModified);
        }

        [Test]
        public void should_modify_audio_and_set_nullables()
        {
            var audio = _testData.Audios.Create(x =>
            {
                x.Released = DateTime.Now;
                x.Purchased = DateTime.Now;
                x.PurchasePrice = 15M;
            }).Audio;

            Http.ForUi.AsPublic(false, false, PutAudioUrl, audio.Id)
                .PutJson(new AudioPutHandler.Request
                {
                    ArtistId = audio.Artist.Id,
                    Title = audio.Title,
                    MediaFormat = (int)audio.MediaFormat,
                    Released = null,
                    Purchased = null,
                    PurchasePrice = null,
                    MediaCount = audio.MediaCount
                })
                .Status.ShouldEqual(HttpStatusCode.OK);

            _testData.Session.Refresh(audio);

            audio.Released.ShouldBeNull();
            audio.Purchased.ShouldBeNull();
            audio.PurchasePrice.ShouldBeNull();
        }

        [Test]
        public void should_return_not_found_when_artist_does_not_exist()
        {
            Http.ForUi.AsPublic(false, false, PutAudioUrl, _testData.Concerts.Create().Concert.Id)
                .PutJson(new AudioPutHandler.Request
                {
                    ArtistId = Guid.NewGuid()
                })
                .Status.ShouldEqual(HttpStatusCode.NotFound);
        }
    }
}