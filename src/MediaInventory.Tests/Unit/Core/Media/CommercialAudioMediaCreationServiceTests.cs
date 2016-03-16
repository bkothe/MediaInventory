using System;
using MediaInventory.Core.Media;
using MediaInventory.Infrastructure.Common.Data.Orm;
using MediaInventory.Infrastructure.Common.Exceptions;
using MediaInventory.Tests.Common.Fakes.Data;
using NSubstitute;
using NUnit.Framework;
using Ploeh.AutoFixture.NUnit2;
using Should;

namespace MediaInventory.Tests.Unit.Core.Media
{
    [TestFixture]
    public class CommercialAudioMediaCreationServiceTests
    {
        private MemoryRepository<CommercialAudioMedia> _commercialAudioMediae;
        private MemoryRepository<MediaInventory.Core.Artist.Artist> _artists;
        private MediaInventory.Core.Artist.Artist _artist;
        private CommercialAudioMediaValidator _commercialAudioMediaValidator;
        private CommercialAudioMediaCreationService _commercialAudioMediaCreationService;

        [SetUp]
        public void SetUp()
        {
            _artist = new MediaInventory.Core.Artist.Artist { Id = Guid.NewGuid() };
            _commercialAudioMediae = new MemoryRepository<CommercialAudioMedia>(x => x.Id);
            _artists = new MemoryRepository<MediaInventory.Core.Artist.Artist>(x => x.Id, _artist);
            _commercialAudioMediaValidator = new CommercialAudioMediaValidator(_artists);
            _commercialAudioMediaCreationService = new CommercialAudioMediaCreationService(_commercialAudioMediae, _artists, _commercialAudioMediaValidator);
        }

        [Test, AutoData]
        public void should_add_media_to_repository(CommercialAudioMedia autoMedia)
        {
            var commercialAudioMediae = Substitute.For<IRepository<CommercialAudioMedia>>();
            var commercialAudioMediaCreationService = new CommercialAudioMediaCreationService(commercialAudioMediae, _artists, new CommercialAudioMediaValidator(_artists));

            var commercialAudioMedia = commercialAudioMediaCreationService.Create(_artist.Id, autoMedia.Title, autoMedia.MediaFormat,
                autoMedia.Released, autoMedia.Purchased, autoMedia.PurchasePrice, autoMedia.PurchaseLocation, autoMedia.MediaCount, autoMedia.Notes);

            commercialAudioMediae.Received(1).Add(Arg.Is<CommercialAudioMedia>(x => x.Id == commercialAudioMedia.Id));
        }

        [Test, AutoData]
        public void should_create_media(CommercialAudioMedia autoMedia)
        {
            var commercialAudioMedia = _commercialAudioMediaCreationService.Create(_artist.Id, autoMedia.Title, autoMedia.MediaFormat,
                autoMedia.Released, autoMedia.Purchased, autoMedia.PurchasePrice, autoMedia.PurchaseLocation, autoMedia.MediaCount, autoMedia.Notes);

            commercialAudioMedia.Artist.ShouldEqual(_artist);
            commercialAudioMedia.Id.ShouldNotEqual(Guid.Empty);
            commercialAudioMedia.Title.ShouldEqual(autoMedia.Title);
            commercialAudioMedia.MediaFormat.ShouldEqual(autoMedia.MediaFormat);
            commercialAudioMedia.Released.ShouldEqual(autoMedia.Released);
            commercialAudioMedia.Purchased.ShouldEqual(autoMedia.Purchased);
            commercialAudioMedia.PurchasePrice.ShouldEqual(autoMedia.PurchasePrice);
            commercialAudioMedia.PurchaseLocation.ShouldEqual(autoMedia.PurchaseLocation);
            commercialAudioMedia.MediaCount.ShouldEqual(autoMedia.MediaCount);
            commercialAudioMedia.Notes.ShouldEqual(autoMedia.Notes);
        }

        [Test]
        public void should_throw_validation_exception_for_invalid_parameters()
        {
            Assert.Throws<ValidationException>(() => _commercialAudioMediaCreationService.Create(_artist.Id, "", MediaFormat.Cassette, null, null, null, null, 0, null));
        }
    }
}