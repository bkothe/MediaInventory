using System;
using MediaInventory.Core.Artist;
using MediaInventory.Core.Media;
using MediaInventory.Tests.Common.Extensions;
using MediaInventory.Tests.Common.Fakes.Data;
using MediaInventory.UI.api.media.audio;
using NSubstitute;
using NUnit.Framework;
using Ploeh.AutoFixture.NUnit2;
using Should;

namespace MediaInventory.Tests.Unit.Ui.api.media.audio
{
    [TestFixture]
    public class AudioPutHandlerTests
    {
        [Test]
        public void should_not_create_artist_when_artist_with_specified_name_exists()
        {
            var artistName = RandomString.GenerateAlphaNumeric();
            var artistCreationService = Substitute.For<IArtistCreationService>();

            new AudioPutHandler(Substitute.For<IAudioModificationService>(),
                new MemoryRepository<Artist>(x => x.Id) { new Artist { Name = artistName } },
                artistCreationService).Execute_AudioId(new AudioPutHandler.Request
                {
                    ArtistName = artistName,
                });

            artistCreationService.DidNotReceive().Create(Arg.Any<string>());
        }

        [Test]
        public void should_call_create_artist_when_artist_with_specified_name_does_not_exist()
        {
            var artistCreationService = Substitute.For<IArtistCreationService>();
            var artistName = RandomString.GenerateAlphaNumeric();

            new AudioPutHandler(Substitute.For<IAudioModificationService>(), new MemoryRepository<Artist>(x => x.Id),
                artistCreationService).Execute_AudioId(new AudioPutHandler.Request { ArtistName = artistName });

            artistCreationService.Received().Create(Arg.Is(artistName));
        }

        [Test]
        public void should_call_audio_modification_service_modify()
        {
            var audioModificationService = Substitute.For<IAudioModificationService>();
            var audioId = Guid.NewGuid();

            new AudioPutHandler(audioModificationService, new MemoryRepository<Artist>(x => x.Id),
                Substitute.For<IArtistCreationService>()).Execute_AudioId(new AudioPutHandler.Request {
                    AudioId = audioId, ArtistName = RandomString.GenerateAlphaNumeric() });

            audioModificationService.Received(1).Modify(Arg.Is(audioId), Arg.Any<Action<Audio>>());
        }

        [Test, AutoData]
        public void should_modify_existing_audio(Audio autoAudio)
        {
            var artists = new MemoryRepository<Artist>(x => x.Id);
            var audios = new MemoryRepository<Audio>(x => x.Id);
            var artist = artists.Add(new Artist { Name = "Rush" }); 
            var existingAudio = audios.Add(new Audio());

            new AudioPutHandler(new AudioModificationService(audios, new AudioValidator(artists)), artists,
                new ArtistCreationService(artists, new ArtistValidator(artists)))
                .Execute_AudioId(new AudioPutHandler.Request
                {
                    AudioId = existingAudio.Id,
                    ArtistName = artist.Name,
                    Title = autoAudio.Title,
                    MediaFormat = autoAudio.MediaFormat,
                    Released = autoAudio.Released,
                    Purchased = autoAudio.Purchased,
                    PurchasePrice = autoAudio.PurchasePrice,
                    PurchaseLocation = autoAudio.PurchaseLocation,
                    MediaCount = autoAudio.MediaCount,
                    Notes = autoAudio.Notes
                });

            var modifiedAudio = audios.Get(existingAudio.Id);
            modifiedAudio.Artist.ShouldEqual(artist);
            modifiedAudio.Title.ShouldEqual(autoAudio.Title);
            modifiedAudio.MediaFormat.ShouldEqual(autoAudio.MediaFormat);
            modifiedAudio.Released.ShouldEqual(autoAudio.Released);
            modifiedAudio.Purchased.ShouldEqual(autoAudio.Purchased);
            modifiedAudio.PurchasePrice.ShouldEqual(autoAudio.PurchasePrice);
            modifiedAudio.PurchaseLocation.ShouldEqual(autoAudio.PurchaseLocation);
            modifiedAudio.MediaCount.ShouldEqual(autoAudio.MediaCount);
            modifiedAudio.Notes.ShouldEqual(autoAudio.Notes);
        }
    }
}