using System;
using MediaInventory.Core.Performance;
using MediaInventory.Infrastructure.Common.Data.Orm;
using MediaInventory.Infrastructure.Common.Exceptions;
using MediaInventory.Tests.Common.Fakes.Data;
using NSubstitute;
using NUnit.Framework;
using Should;

namespace MediaInventory.Tests.Unit.Core.Performance
{
    [TestFixture]
    public class ConcertCreationServiceTests
    {
        private ConcertCreationService _concertCreationService;
        private ConcertValidator _concertValidator;
        private IRepository<Concert> _concerts;
        private IRepository<MediaInventory.Core.Artist.Artist> _artists;
        private IRepository<MediaInventory.Core.Venue.Venue> _venues;
        private MediaInventory.Core.Artist.Artist _artist;
        private MediaInventory.Core.Venue.Venue _venue;

        [SetUp]
        public void SetUp()
        {
            _concerts = new MemoryRepository<Concert>(x => x.Id);
            _artists = new MemoryRepository<MediaInventory.Core.Artist.Artist>(x => x.Id);
            _venues = new MemoryRepository<MediaInventory.Core.Venue.Venue>(x => x.Id);
            _concertValidator = new ConcertValidator(_artists, _venues);
            _concertCreationService = new ConcertCreationService(_concerts, _artists, _venues, _concertValidator);
            _venue = _venues.Add(new MediaInventory.Core.Venue.Venue { Id = Guid.NewGuid() });
            _artist = _artists.Add(new MediaInventory.Core.Artist.Artist());
        }

        [Test]
        public void should_add_concert_to_repository()
        {
            var concerts = Substitute.For<IRepository<Concert>>();
            var concertCreationService = new ConcertCreationService(concerts, _artists, _venues, _concertValidator);

            var concert = concertCreationService.Create(_artist.Id, DateTime.Now, _venue.Id);

            concerts.Received(1).Add(Arg.Is<Concert>(x => x.Id == concert.Id));
        }

        [Test]
        public void should_return_concert_with_correct_properties()
        {
            var date = DateTime.Now;

            var concert = _concertCreationService.Create(_artist.Id, DateTime.Now, _venue.Id);

            concert.Id.ShouldNotEqual(Guid.Empty);
            concert.Artist.ShouldEqual(_artist);
            concert.Venue.ShouldEqual(_venue);
            concert.Date.ShouldEqual(date);
        }

        [Test]
        public void should_throw_not_found_exception_when_artist_does_not_exist()
        {
            Assert.Throws<NotFoundException>(() => _concertCreationService
                .Create(Guid.NewGuid(), DateTime.Now, _venue.Id));
        }

        [Test]
        public void should_throw_not_found_exception_when_venue_does_not_exist()
        {
            Assert.Throws<NotFoundException>(() => _concertCreationService
                .Create(_artist.Id, DateTime.Now, Guid.NewGuid()));
        }
    }
}