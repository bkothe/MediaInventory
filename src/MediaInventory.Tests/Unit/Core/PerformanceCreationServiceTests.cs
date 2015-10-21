using System;
using FluentValidation;
using MediaInventory.Core;
using MediaInventory.Infrastructure.Framework.Data.Orm;
using MediaInventory.Tests.Common.Fakes.Data;
using NSubstitute;
using NUnit.Framework;
using Should;

namespace MediaInventory.Tests.Unit.Core
{
    [TestFixture]
    public class PerformanceCreationServiceTests
    {
        private ConcertCreationService _concertCreationService;
        private ConcertValidator _concertValidator;
        private IRepository<Concert> _concerts;
        private IRepository<Artist> _artists;

        [SetUp]
        public void SetUp()
        {
            _concerts = new MemoryRepository<Concert>(x => x.Id);
            _artists = new MemoryRepository<Artist>(x => x.Id);
            _concertValidator = new ConcertValidator(_artists);
            _concertCreationService = new ConcertCreationService(_concerts, _concertValidator);
        }

        [Test]
        public void should_add_concert_to_repository()
        {
            var concerts = Substitute.For<IRepository<Concert>>();
            var concertCreationService = new ConcertCreationService(concerts, _concertValidator);

            var concert = concertCreationService.Create(_artists.Add(new Artist { Id = Guid.NewGuid() }), DateTime.Now);

            concerts.Received(1).Add(Arg.Is(concert));
        }

        [Test]
        public void should_return_concert_with_correct_properties()
        {
            var artist = _artists.Add(new Artist { Id = Guid.NewGuid() });
            var date = DateTime.Now;

            var performance = _concertCreationService.Create(artist, date);

            performance.Id.ShouldNotEqual(Guid.Empty);
            performance.Artist.ShouldEqual(artist);
            performance.Date.ShouldEqual(date);
        }

        [Test, ExpectedException(typeof(ValidationException))]
        public void should_throw_exception_when_artist_does_not_exist()
        {
            _concertCreationService.Create(new Artist(), DateTime.Now);
        }
    }
}