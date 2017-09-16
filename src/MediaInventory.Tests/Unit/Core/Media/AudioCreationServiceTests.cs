using System;
using MediaInventory.Core.Media;
using MediaInventory.Infrastructure.Common.Data.Orm;
using MediaInventory.Infrastructure.Common.Exceptions;
using MediaInventory.Tests.Common.Fakes.Data;
using NSubstitute;
using NUnit.Framework;
using Ploeh.AutoFixture.NUnit3;
using Should;

namespace MediaInventory.Tests.Unit.Core.Media
{
    [TestFixture]
    public class AudioCreationServiceTests
    {
        private MemoryRepository<Audio> _audios;
        private MemoryRepository<MediaInventory.Core.Artist.Artist> _artists;
        private MediaInventory.Core.Artist.Artist _artist;
        private AudioValidator _audioValidator;
        private AudioCreationService _audioCreationService;

        [SetUp]
        public void SetUp()
        {
            _artist = new MediaInventory.Core.Artist.Artist { Id = Guid.NewGuid() };
            _audios = new MemoryRepository<Audio>(x => x.Id);
            _artists = new MemoryRepository<MediaInventory.Core.Artist.Artist>(x => x.Id, _artist);
            _audioValidator = new AudioValidator(_artists);
            _audioCreationService = new AudioCreationService(_audios, _audioValidator);
        }

        [Test, AutoData]
        public void should_add_audio_to_repository(Audio autoAudio)
        {
            var audios = Substitute.For<IRepository<Audio>>();
            var audioCreationService = new AudioCreationService(audios, new AudioValidator(_artists));

            var audio = audioCreationService.Create(_artist, autoAudio.Title, autoAudio.MediaFormat,
                autoAudio.Released, autoAudio.Purchased, autoAudio.PurchasePrice, autoAudio.PurchaseLocation, autoAudio.MediaCount, autoAudio.Notes);

            audios.Received(1).Add(Arg.Is<Audio>(x => x.Id == audio.Id));
        }

        [Test, AutoData]
        public void should_create_audio(Audio autoAudio)
        {
            var audio = _audioCreationService.Create(_artist, autoAudio.Title, autoAudio.MediaFormat,
                autoAudio.Released, autoAudio.Purchased, autoAudio.PurchasePrice, autoAudio.PurchaseLocation, autoAudio.MediaCount, autoAudio.Notes);

            audio.Artist.ShouldEqual(_artist);
            audio.Id.ShouldNotEqual(Guid.Empty);
            audio.Title.ShouldEqual(autoAudio.Title);
            audio.MediaFormat.ShouldEqual(autoAudio.MediaFormat);
            audio.Released.ShouldEqual(autoAudio.Released);
            audio.Purchased.ShouldEqual(autoAudio.Purchased);
            audio.PurchasePrice.ShouldEqual(autoAudio.PurchasePrice);
            audio.PurchaseLocation.ShouldEqual(autoAudio.PurchaseLocation);
            audio.MediaCount.ShouldEqual(autoAudio.MediaCount);
            audio.Notes.ShouldEqual(autoAudio.Notes);
        }

        [Test]
        public void should_throw_validation_exception_for_invalid_parameters()
        {
            Assert.Throws<ValidationException>(() => _audioCreationService.Create(_artist, "", MediaFormat.Cassette, null, null, null, null, 0, null));
        }
    }
}