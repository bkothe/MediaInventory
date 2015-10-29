using System;
using MediaInventory.Core.Venue;
using MediaInventory.Infrastructure.Framework.Data.Orm;
using MediaInventory.Tests.Common.Extensions;
using MediaInventory.Tests.Common.Fakes.Data;
using NSubstitute;
using NUnit.Framework;
using Should;

namespace MediaInventory.Tests.Unit.Core.Venue
{
    [TestFixture]
    public class VenueCreationServiceTests
    {
        private VenueCreationService _venueCreationService;
        private VenueValidator _venueValidator;
        private IRepository<MediaInventory.Core.Venue.Venue> _venues;

        [SetUp]
        public void SetUp()
        {
            _venues = new MemoryRepository<MediaInventory.Core.Venue.Venue>(x => x.Id);
            _venueValidator = new VenueValidator(_venues);
            _venueCreationService = new VenueCreationService(_venues, _venueValidator);
        }

        [Test]
        public void should_add_venue_to_repository()
        {
            var venues = Substitute.For<IRepository<MediaInventory.Core.Venue.Venue>>();
            var venueCreationService = new VenueCreationService(venues, _venueValidator);

            var venue = venueCreationService.Create(RandomString.GenerateAlphaNumeric(),
                RandomString.GenerateAlphaNumeric(), RandomString.GenerateAlphaNumeric(2));

            venues.Received(1).Add(Arg.Is(venue));
        }

        [Test]
        public void should_return_venue_with_correct_properties()
        {
            const string name = "The Vic";
            const string city = "Chicago";
            const string state = "IL";

            var venue = _venueCreationService.Create(name, city, state);

            venue.Id.ShouldNotEqual(Guid.Empty);
            venue.Name.ShouldEqual(name);
            venue.City.ShouldEqual(city);
            venue.State.ShouldEqual(state);
        }
    }
}