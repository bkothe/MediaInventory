using FluentValidation.TestHelper;
using MediaInventory.Core.Performance;
using MediaInventory.Tests.Common.Fakes.Data;
using NUnit.Framework;
using Should;

namespace MediaInventory.Tests.Unit.Core.Performance
{
    [TestFixture]
    public class ConcertValidatorTests
    {
        private ConcertValidator _concertValidator;
        private MemoryRepository<MediaInventory.Core.Artist.Artist> _artists;
        private MemoryRepository<MediaInventory.Core.Venue.Venue> _venues;

        [SetUp]
        public void SetUp()
        {
            _artists = new MemoryRepository<MediaInventory.Core.Artist.Artist>(x => x.Id);
            _venues = new MemoryRepository<MediaInventory.Core.Venue.Venue>(x => x.Id);
            _concertValidator = new ConcertValidator(_artists, _venues);
        }

        [Test]
        public void should_not_have_error_for_valid_concert()
        {
            _concertValidator.Validate(new Concert
            {
                Artist = _artists.Add(new MediaInventory.Core.Artist.Artist()),
                Venue = _venues.Add(new MediaInventory.Core.Venue.Venue())
            }).IsValid.ShouldBeTrue();
        }

        // artist
        [Test]
        public void should_have_error_when_artist_is_null()
        {
            _concertValidator.ShouldHaveValidationErrorFor(x => x.Artist, new Concert { Artist = null });
        }

        [Test]
        public void should_have_error_when_artist_does_not_exist()
        {
            _concertValidator.ShouldHaveValidationErrorFor(x => x.Artist, new MediaInventory.Core.Artist.Artist());
        }

        [Test]
        public void should_not_have_error_when_artist_exists()
        {
            _concertValidator.ShouldNotHaveValidationErrorFor(x => x.Artist, _artists.Add(new MediaInventory.Core.Artist.Artist()));
        }

        // venue
        [Test]
        public void should_have_error_when_venue_is_null()
        {
            _concertValidator.ShouldHaveValidationErrorFor(x => x.Venue, new Concert { Venue = null });
        }

        [Test]
        public void should_have_error_when_venue_does_not_exist()
        {
            _concertValidator.ShouldHaveValidationErrorFor(x => x.Venue, new MediaInventory.Core.Venue.Venue());
        }

        [Test]
        public void should_not_have_error_when_venue_exists()
        {
            _concertValidator.ShouldNotHaveValidationErrorFor(x => x.Venue, _venues.Add(new MediaInventory.Core.Venue.Venue()));
        }
    }
}