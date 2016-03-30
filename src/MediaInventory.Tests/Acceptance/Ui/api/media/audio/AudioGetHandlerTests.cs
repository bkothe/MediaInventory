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
            const string artistName = "Rush";
            const string title = "2112";
            const MediaFormat mediaFormat = MediaFormat.Vinyl;
            DateTime released = DateTime.Now.AddDays(-10);
            DateTime purchased = DateTime.Now.AddDays(-9);
            const decimal purchasePrice = (decimal)8.95;
            const string purchaseLocation = "amazon.com";
            const int mediaCount = 1;
            const string notes = "Notes about 2112";

            _testData.Audios.Create(x =>
            {
                x.Artist.Name = artistName;
                x.Title = title;
                x.MediaFormat = mediaFormat;
                x.Released = released;
                x.Purchased = purchased;
                x.PurchasePrice = purchasePrice;
                x.PurchaseLocation = purchaseLocation;
                x.MediaCount = mediaCount;
                x.Notes = notes;
            });

            var response = Http.ForUi.AsPublic(false, false, GetAudioUrl,
                _testData.Tracking.Audios.Single().Id).GetJson<AudioModel>();

            response.Status.ShouldEqual(HttpStatusCode.OK);
            response.Data.Id.ShouldEqual(_testData.Tracking.Audios.Single().Id);
            response.Data.ArtistId.ShouldNotEqual(Guid.Empty);
            response.Data.ArtistName.ShouldEqual(artistName);
            response.Data.Title.ShouldEqual(title);
            response.Data.MediaFormat.ShouldEqual(mediaFormat);
            response.Data.Released.ShouldEqual(released);
            response.Data.Purchased.ShouldEqual(purchased);
            response.Data.PurchasePrice.ShouldEqual(purchasePrice);
            response.Data.PurchaseLocation.ShouldEqual(purchaseLocation);
            response.Data.MediaCount.ShouldEqual(mediaCount);
            response.Data.Notes.ShouldEqual(notes);
        }

        [Test]
        public void should_return_not_found()
        {
            Http.ForUi.AsPublic(false, false, GetAudioUrl, Guid.NewGuid())
                .GetJson<AudioModel>().Status.ShouldEqual(HttpStatusCode.NotFound);
        }
    }
}