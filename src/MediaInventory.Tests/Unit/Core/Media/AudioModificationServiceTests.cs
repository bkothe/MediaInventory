using System;
using MediaInventory.Core.Media;
using MediaInventory.Infrastructure.Common.Exceptions;
using MediaInventory.Tests.Common.Fakes.Data;
using NUnit.Framework;
using Should;

namespace MediaInventory.Tests.Unit.Core.Media
{
    [TestFixture]
    public class AudioModificationServiceTests
    {
        private readonly Guid OldArtistId = Guid.NewGuid();
        private readonly Guid NewArtistId = Guid.NewGuid();
        private const string OldTitle = "2113";
        private const string NewTitle = "2112";
        private readonly MediaFormat OldMediaFormat = MediaFormat.Vinyl;
        private readonly MediaFormat NewMediaFormat = MediaFormat.Shn;
        private readonly DateTime OldReleased = DateTime.Parse("3/22/2000");
        private readonly DateTime NewReleased = DateTime.Parse("3/22/2012");
        private readonly DateTime OldPurchased = DateTime.Parse("3/16/2012");
        private readonly DateTime NewPurchased = DateTime.Parse("3/16/2012");
        private const decimal OldPurchasePrice = 15M;
        private const decimal NewPurchasePrice = 10M;
        private const string OldPurchaseLocation = "Tower Records";
        private const string NewPurchaseLocation = "Kiss The Sky";
        private const int OldMediaCount = 1;
        private const int NewMediaCount = 2;
        private const string OldNotes = "My old notes";
        private const string NewNotes = "My new notes";

        private MemoryRepository<Audio> _audios;
        private MemoryRepository<MediaInventory.Core.Artist.Artist> _artists;
        private AudioModificationService _audioModificationService;

        [SetUp]
        public void SetUp()
        {
            _artists = new MemoryRepository<MediaInventory.Core.Artist.Artist>(x => x.Id,
                new MediaInventory.Core.Artist.Artist { Id = OldArtistId },
                new MediaInventory.Core.Artist.Artist { Id = NewArtistId });
            _audios = new MemoryRepository<Audio>(x => x.Id);
            _audioModificationService = new AudioModificationService(_audios,
                new AudioValidator(_artists));
        }

        [Test]
        public void should_modify_audio()
        {
            var audio = _audios.Add(new Audio
            {
                Artist = _artists.Get(OldArtistId),
                Title = OldTitle,
                MediaFormat = OldMediaFormat,
                Released = OldReleased,
                Purchased = OldPurchased,
                PurchasePrice = OldPurchasePrice,
                PurchaseLocation = OldPurchaseLocation,
                MediaCount = OldMediaCount,
                Notes = OldNotes
            });

            _audioModificationService.Modify(audio.Id, x =>
            {
                x.Artist = _artists.Get(NewArtistId);
                x.Title = NewTitle;
                x.MediaFormat = NewMediaFormat;
                x.Released = NewReleased;
                x.Purchased = NewPurchased;
                x.PurchasePrice = NewPurchasePrice;
                x.PurchaseLocation = NewPurchaseLocation;
                x.MediaCount = NewMediaCount;
                x.Notes = NewNotes;
            });

            audio.Artist.Id.ShouldEqual(NewArtistId);
            audio.Title.ShouldEqual(NewTitle);
            audio.MediaFormat.ShouldEqual(NewMediaFormat);
            audio.Released.ShouldEqual(NewReleased);
            audio.Purchased.ShouldEqual(NewPurchased);
            audio.PurchasePrice.ShouldEqual(NewPurchasePrice);
            audio.PurchaseLocation.ShouldEqual(NewPurchaseLocation);
            audio.MediaCount.ShouldEqual(NewMediaCount);
            audio.Notes.ShouldEqual(NewNotes);
        }

        [Test]
        public void should_throw_not_found_when_audio_does_not_exist()
        {
            Assert.Throws<NotFoundException>(() => _audioModificationService.Modify(Guid.NewGuid(), null));
        }

        [Test]
        public void should_throw_validation_exception_when_artist_does_not_exist()
        {
            var audio = _audios.Add(new Audio
            {
                Artist = _artists.Get(OldArtistId),
                Title = OldTitle,
                MediaFormat = OldMediaFormat,
                MediaCount = OldMediaCount
            });

            Assert.Throws<ValidationException>(() => _audioModificationService.Modify(audio.Id, x =>
            {
                x.Artist = new MediaInventory.Core.Artist.Artist { Id = Guid.NewGuid() };
            }));
        }
    }
}