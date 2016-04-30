using MediaInventory.Core.Venue;
using MediaInventory.Tests.Common.Extensions;
using MediaInventory.Tests.Common.Fakes.Data;
using MediaInventory.UI.Core;
using NSubstitute;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Should;

namespace MediaInventory.Tests.Unit.Ui.Core
{
    [TestFixture]
    public class VenueResolverServiceTests
    {
        private MemoryRepository<Venue> _venues; 
        private VenueResolverService _venueResolverService;
        private IVenueCreationService _venueCreationService;

        [SetUp]
        public void SetUp()
        {
            _venues = new MemoryRepository<Venue>(x => x.Id);
            _venueCreationService = Substitute.For<IVenueCreationService>();
            _venueResolverService = new VenueResolverService(_venues, _venueCreationService);
        }

        [Test]
        public void should_return_existing_venue()
        {
            var autoVenue = new Fixture().Build<Venue>()
                .Without(x => x.PreviousVenue)
                .Create();

            var result = _venueResolverService.ResolveVenue(_venues.Add(autoVenue).Name);
            
            result.Id.ShouldEqual(autoVenue.Id);
            result.Name.ShouldEqual(autoVenue.Name);
            result.City.ShouldEqual(autoVenue.City);
            result.State.ShouldEqual(autoVenue.State);
        }

        [Test]
        public void should_not_call_create_when_venue_with_specified_name_exists()
        {
            _venueResolverService.ResolveVenue(_venues.Add(new Venue { Name = RandomString.GenerateAlphaNumeric() }).Name);

            _venueCreationService.DidNotReceiveWithAnyArgs().Create(Arg.Any<string>());
        }

        [Test]
        public void should_call_create_when_venue_with_specified_name_does_not_exist()
        {
            var venueName = RandomString.GenerateAlphaNumeric();

            _venueResolverService.ResolveVenue(venueName);

            _venueCreationService.Received(1).Create(venueName);
        }
    }
}