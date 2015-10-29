using System.Linq;
using MediaInventory.Core.Venue;
using MediaInventory.Tests.Common.Fakes.Data;
using NUnit.Framework;
using Should;

namespace MediaInventory.Tests.Unit.Core.Venue
{
    [TestFixture]
    public class VenueDeletionServiceTests
    {
        private MemoryRepository<MediaInventory.Core.Venue.Venue> _venues;
        private VenueDeletionService _venueDeletionService;

        [SetUp]
        public void SetUp()
        {
            _venues = new MemoryRepository<MediaInventory.Core.Venue.Venue>(x => x.Id);
            _venueDeletionService = new VenueDeletionService(_venues);
        }

        [Test]
        public void should_delete_venue()
        {
            var venue = _venues.Add(new MediaInventory.Core.Venue.Venue());
            _venues.Count(x => x.Id == venue.Id).ShouldEqual(1);

            _venueDeletionService.Delete(venue.Id);

            _venues.Count(x => x.Id == venue.Id).ShouldEqual(0);
        }
    }
}