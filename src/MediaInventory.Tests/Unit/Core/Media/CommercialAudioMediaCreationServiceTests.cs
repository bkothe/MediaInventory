using System;
using MediaInventory.Core.Media;
using MediaInventory.Infrastructure.Framework.Data.Orm;
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
        private CommercialAudioMediaCreationService _sut;

        [SetUp]
        public void SetUp()
        {
            _commercialAudioMediae = new MemoryRepository<CommercialAudioMedia>(x => x.Id);
            _sut = new CommercialAudioMediaCreationService(_commercialAudioMediae);
        }

        [Test, AutoData]
        public void should_add_media_to_repository(CommercialAudioMedia fakeMedia)
        {
            var sut = Substitute.For<IRepository<CommercialAudioMedia>>();
            var commercialAudioMediaCreationService = new CommercialAudioMediaCreationService(sut);

            var cd = commercialAudioMediaCreationService.Create(fakeMedia.Artist, fakeMedia.Title, fakeMedia.MediaFormat,
                fakeMedia.Released, fakeMedia.Purchased, fakeMedia.PurchasePrice, fakeMedia.PurchaseLocation, fakeMedia.MediaCount, fakeMedia.Notes);

            sut.Received(1).Add(Arg.Is(cd));
        }

        [Test, AutoData]
        public void should_create_media(CommercialAudioMedia fakeMedia)
        {
            var cd = _sut.Create(fakeMedia.Artist, fakeMedia.Title, fakeMedia.MediaFormat,
                fakeMedia.Released, fakeMedia.Purchased, fakeMedia.PurchasePrice, fakeMedia.PurchaseLocation, fakeMedia.MediaCount, fakeMedia.Notes);

            cd.Artist.ShouldEqual(fakeMedia.Artist);
            cd.Id.ShouldNotEqual(Guid.Empty);
            cd.Title.ShouldEqual(fakeMedia.Title);
            cd.MediaFormat.ShouldEqual(fakeMedia.MediaFormat);
            cd.Released.ShouldEqual(fakeMedia.Released);
            cd.Purchased.ShouldEqual(fakeMedia.Purchased);
            cd.PurchasePrice.ShouldEqual(fakeMedia.PurchasePrice);
            cd.PurchaseLocation.ShouldEqual(fakeMedia.PurchaseLocation);
            cd.MediaCount.ShouldEqual(fakeMedia.MediaCount);
            cd.Notes.ShouldEqual(fakeMedia.Notes);
        }
    }
}