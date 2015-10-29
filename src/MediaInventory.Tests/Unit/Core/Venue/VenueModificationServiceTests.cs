using System;
using MediaInventory.Core.Venue;
using MediaInventory.Infrastructure.Framework.Exceptions;
using MediaInventory.Tests.Common.Fakes.Data;
using NUnit.Framework;
using Should;

namespace MediaInventory.Tests.Unit.Core.Venue
{
    [TestFixture]
    public class VenueModificationServiceTests
    {
        private const string OldName = "The Vic";
        private const string NewName = "Aragon Ballroom";
        private const string OldCity = "Phoenix";
        private const string NewCity = "Chicago";
        private const string OldState = "AZ";
        private const string NewState = "IL"; 

        private MemoryRepository<MediaInventory.Core.Venue.Venue> _venues;
        private VenueModificationService _venueModificationService;

        [SetUp]
        public void SetUp()
        {
            _venues = new MemoryRepository<MediaInventory.Core.Venue.Venue>(x => x.Id);
            _venueModificationService = new VenueModificationService(_venues,
                new VenueValidator(_venues));
        }

        [Test]
        public void should_modify_venue()
        {
            var venue = _venues.Add(new MediaInventory.Core.Venue.Venue
            {
                Name = OldName,
                City = OldCity,
                State = OldState
            });

            _venueModificationService.Modify(venue.Id, x =>
            {
                x.Name = NewName;
                x.City = NewCity;
                x.State = NewState;
            });

            venue.Name.ShouldEqual(NewName);
            venue.City.ShouldEqual(NewCity);
            venue.State.ShouldEqual(NewState);
        }

        [Test, ExpectedException(typeof(NotFoundException))]
        public void should_throw_not_found_when_venue_does_not_exist()
        {
            _venueModificationService.Modify(Guid.NewGuid(), null);
        }
    }
}