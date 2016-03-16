using System;
using System.Net;
using MediaInventory.Core.Media;
using MediaInventory.Tests.Acceptance.Common.Http;
using MediaInventory.Tests.Common.Data.TestData;
using MediaInventory.Tests.Common.Extensions;
using MediaInventory.UI.api.media.audio;
using NUnit.Framework;
using Should;

namespace MediaInventory.Tests.Acceptance.Ui.api.media
{
    [TestFixture]
    public class CommercialAudioMediaPostHandlerTests
    {
        private const string PostAudioUrl = "api/media/audio";

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
            const string title = "2112";
            const MediaFormat mediaFormat = MediaFormat.Vinyl;
            DateTime released = DateTime.Now.AddDays(-10);
            DateTime purchased = DateTime.Now.AddDays(-9);
            const decimal purchasePrice = 8.95M;
            const string purchaseLocation = "amazon.com";
            const int mediaCount = 1;
            const string notes = "Notes about 2112";

            var artist = _testData.Artists.Create().Artist;

            var request = new CommercialAudioMediaPostHandler.Request
            {
                ArtistId = artist.Id,
                Title = title,
                MediaFormat = mediaFormat,
                Released = released,
                Purchased = purchased,
                PurchasePrice = purchasePrice,
                PurchaseLocation = purchaseLocation,
                MediaCount = mediaCount,
                Notes = notes
            };

            var response = Http.ForUi.AsPublic(false, false, PostAudioUrl)
                .PostJson<CommercialAudioMediaPostHandler.Request, CommercialAudioMediaModel>(request);

            _testData.IncludeInCleanUp(_testData.Repositories.CommercialAudioMediaRepository.Get(response.Data.Id));

            response.Status.ShouldEqual(HttpStatusCode.OK);
            response.Data.Id.ShouldNotEqual(Guid.Empty);
            response.Data.ArtistName.ShouldEqual(artist.Name);
            response.Data.ArtistId.ShouldEqual(artist.Id);
            response.Data.Title.ShouldEqual(title);
            response.Data.MediaFormat.ShouldEqual(mediaFormat);
            response.Data.Released.ShouldBeWithinSeconds(released, 1);
            response.Data.Purchased.ShouldBeWithinSeconds(purchased, 1);
            response.Data.PurchasePrice.ShouldEqual(purchasePrice);
            response.Data.PurchaseLocation.ShouldEqual(purchaseLocation);
            response.Data.MediaCount.ShouldEqual(mediaCount);
            response.Data.Notes.ShouldEqual(notes);
        }
    }
}