using System;
using MediaInventory.Core.Performance;
using MediaInventory.Infrastructure.Framework.Exceptions;
using MediaInventory.Tests.Common.Fakes.Data;
using NUnit.Framework;
using Should;

namespace MediaInventory.Tests.Unit.Core.Performance
{
    [TestFixture]
    public class ConcertModificationServiceTests
    {
        private readonly DateTime OldDate = DateTime.Parse("3/22/2000");
        private readonly DateTime NewDate = DateTime.Parse("3/22/2012");
        private readonly Guid OldArtistId = Guid.NewGuid();
        private readonly Guid NewArtistId = Guid.NewGuid();
        private readonly Guid OldVenueId = Guid.NewGuid();
        private readonly Guid NewVenueId = Guid.NewGuid();

        private MemoryRepository<Concert> _concerts;
        private MemoryRepository<MediaInventory.Core.Artist.Artist> _artists;
        private MemoryRepository<MediaInventory.Core.Venue.Venue> _venues;
        private ConcertModificationService _concertModificationService;

        [SetUp]
        public void SetUp()
        {
            _artists = new MemoryRepository<MediaInventory.Core.Artist.Artist>(x => x.Id,
                new MediaInventory.Core.Artist.Artist { Id = OldArtistId },
                new MediaInventory.Core.Artist.Artist { Id = NewArtistId });
            _venues = new MemoryRepository<MediaInventory.Core.Venue.Venue>(x => x.Id,
                new MediaInventory.Core.Venue.Venue { Id = OldVenueId },
                new MediaInventory.Core.Venue.Venue { Id = NewVenueId });
            _concerts = new MemoryRepository<Concert>(x => x.Id);
            _concertModificationService = new ConcertModificationService(_concerts,
                new ConcertValidator(_artists, _venues));
        }

        [Test]
        public void should_modify_concert()
        {
            var concert = _concerts.Add(new Concert
            {
                Artist = _artists.Get(OldArtistId),
                Venue = _venues.Get(OldVenueId),
                Date = OldDate
            });

            _concertModificationService.Modify(concert.Id, x =>
            {
                x.Artist = _artists.Get(NewArtistId);
                x.Venue = _venues.Get(NewVenueId);
                x.Date = NewDate;
            });

            concert.Artist.Id.ShouldEqual(NewArtistId);
            concert.Venue.Id.ShouldEqual(NewVenueId);
            concert.Date.ShouldEqual(NewDate);
        }

        [Test, ExpectedException(typeof(NotFoundException))]
        public void should_throw_not_found_when_concert_does_not_exist()
        {
            _concertModificationService.Modify(Guid.NewGuid(), null);
        }

        [Test, ExpectedException(typeof(ValidationException))]
        public void should_throw_validation_exception_when_artist_does_not_exist()
        {
            var concert = _concerts.Add(new Concert
            {
                Artist = _artists.Get(OldArtistId),
                Venue = _venues.Get(OldVenueId),
                Date = OldDate
            });

            _concertModificationService.Modify(concert.Id, x =>
            {
                x.Artist = new MediaInventory.Core.Artist.Artist { Id = Guid.NewGuid() };
            });
        }

        [Test, ExpectedException(typeof(ValidationException))]
        public void should_throw_validation_exception_when_venue_does_not_exist()
        {
            var concert = _concerts.Add(new Concert
            {
                Artist = _artists.Get(OldArtistId),
                Venue = _venues.Get(OldVenueId),
                Date = OldDate
            });

            _concertModificationService.Modify(concert.Id, x =>
            {
                x.Venue = new MediaInventory.Core.Venue.Venue { Id = Guid.NewGuid() };
            });
        }
    }
}