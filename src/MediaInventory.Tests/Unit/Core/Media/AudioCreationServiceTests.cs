using System;
using FluentAssertions;
using MediaInventory.Core.Media;
using MediaInventory.Infrastructure.Common.Exceptions;
using MediaInventory.Tests.Common.Fakes.Data;
using NUnit.Framework;
using Ploeh.AutoFixture.NUnit3;

namespace MediaInventory.Tests.Unit.Core.Media
{
    [TestFixture]
    public class AudioCreationServiceTests
    {
        private MemoryRepository<Audio> _audios;
        private MemoryRepository<MediaInventory.Core.Artist.Artist> _artists;
        private MediaInventory.Core.Artist.Artist _artist;
        private AudioCreationService _sut;

        [SetUp]
        public void SetUp()
        {
            _artist = new MediaInventory.Core.Artist.Artist { Id = Guid.NewGuid() };
            _audios = new MemoryRepository<Audio>(x => x.Id);
            _artists = new MemoryRepository<MediaInventory.Core.Artist.Artist>(x => x.Id, _artist);
            _sut = new AudioCreationService(_audios, new AudioValidator(_artists));
        }

        [Test, AutoData]
        public void should_add_audio_to_repository(Audio autoAudio)
        {
            var audio = _sut.Create(_artist, autoAudio.Title, autoAudio.MediaFormat,
                autoAudio.Released, autoAudio.Purchased, autoAudio.PurchasePrice, autoAudio.PurchaseLocation, autoAudio.MediaCount, autoAudio.Notes);

            _audios.Should().ContainSingle(x => x == audio);
        }

        [Test, AutoData]
        public void should_create_audio(Audio expectedAudio)
        {
            var audio = _sut.Create(_artist, expectedAudio.Title, expectedAudio.MediaFormat,
                expectedAudio.Released, expectedAudio.Purchased, expectedAudio.PurchasePrice, expectedAudio.PurchaseLocation, expectedAudio.MediaCount, expectedAudio.Notes);

            audio.Artist.Should().Be(_artist);
            audio.Id.Should().NotBe(Guid.Empty);
            audio.Title.Should().Be(expectedAudio.Title);
            audio.MediaFormat.Should().Be(expectedAudio.MediaFormat);
            audio.Released.Should().Be(expectedAudio.Released);
            audio.Purchased.Should().Be(expectedAudio.Purchased);
            audio.PurchasePrice.Should().Be(expectedAudio.PurchasePrice);
            audio.PurchaseLocation.Should().Be(expectedAudio.PurchaseLocation);
            audio.MediaCount.Should().Be(expectedAudio.MediaCount);
            audio.Notes.Should().Be(expectedAudio.Notes);
        }

        [Test]
        public void should_throw_validation_exception_for_invalid_parameters()
        {
            Action action = () => _sut.Create(_artist, "", MediaFormat.Cassette, null, null, null, null, 0, null);

            action.ShouldThrow<ValidationException>();
        }
    }
}