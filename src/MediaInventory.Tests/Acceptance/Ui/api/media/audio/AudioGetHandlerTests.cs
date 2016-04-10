using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using MediaInventory.Core.Media;
using MediaInventory.Tests.Acceptance.Common.Http;
using MediaInventory.Tests.Common.Data.TestData;
using MediaInventory.Tests.Common.Extensions;
using MediaInventory.UI.api.media.audio;
using NUnit.Framework;
using Should;

namespace MediaInventory.Tests.Acceptance.Ui.api.media.audio
{
    [TestFixture]
    public class AudioGetHandlerTests
    {
        private const string GetAudioUrl = "api/media/audio/{0}";
        private const string EnumerateAudioUrl = "api/media/audio";

        private const string ArtistName = "Rush";
        private const string Title = "2112";
        private const MediaFormat MediaFormat = Core.Media.MediaFormat.Vinyl;
        private readonly DateTime Released = DateTime.Parse("4/1/1976");
        private readonly DateTime Purchased = DateTime.Parse("4/1/1995");
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
        public void should_enumerate_audio()
        {
            var audioId1 = _testData.Audios.Create().Audio.Id;
            var audioId2 = _testData.Audios.Create().Audio.Id;

            var response = Http.ForUi.AsPublic(false, false, EnumerateAudioUrl)
                .GetJson<List<AudioModel>>();

            response.Status.ShouldEqual(HttpStatusCode.OK);
            response.Data.Count.ShouldEqual(2);
            response.Data.Select(x => x.Id).ShouldBeEquivalent(new[] { audioId1, audioId2 });
        }

        [Test]
        public void should_get_audio()
        {
            _testData.Audios.Create(x =>
            {
                x.Artist.Name = ArtistName;
                x.Title = Title;
                x.MediaFormat = MediaFormat;
                x.Released = Released;
                x.Purchased = Purchased;
                x.PurchasePrice = PurchasePrice;
                x.PurchaseLocation = PurchaseLocation;
                x.MediaCount = MediaCount;
                x.Notes = Notes;
            });

            var response = Http.ForUi.AsPublic(false, false, GetAudioUrl,
                _testData.Tracking.Audios.Single().Id).GetJson<AudioModel>();

            response.Status.ShouldEqual(HttpStatusCode.OK);
            response.Data.Id.ShouldEqual(_testData.Tracking.Audios.Single().Id);
            response.Data.ArtistId.ShouldNotEqual(Guid.Empty);
            response.Data.ArtistName.ShouldEqual(ArtistName);
            response.Data.Title.ShouldEqual(Title);
            response.Data.MediaFormat.ShouldEqual(MediaFormat);
            response.Data.Released.ShouldEqual(Released);
            response.Data.Purchased.ShouldEqual(Purchased);
            response.Data.PurchasePrice.ShouldEqual(PurchasePrice);
            response.Data.PurchaseLocation.ShouldEqual(PurchaseLocation);
            response.Data.MediaCount.ShouldEqual(MediaCount);
            response.Data.Notes.ShouldEqual(Notes);
        }

        [Test]
        public void should_return_not_found()
        {
            Http.ForUi.AsPublic(false, false, GetAudioUrl, Guid.NewGuid())
                .GetJson<AudioModel>().Status.ShouldEqual(HttpStatusCode.NotFound);
        }
    }
}