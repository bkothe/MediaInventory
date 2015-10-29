using System.Linq;
using MediaInventory.Core.Performance;
using MediaInventory.Tests.Common.Fakes.Data;
using NUnit.Framework;
using Should;

namespace MediaInventory.Tests.Unit.Core.Performance
{
    [TestFixture]
    public class ConcertDeletionServiceTests
    {
        private MemoryRepository<Concert> _concerts;
        private ConcertDeletionService _concertDeletionService;

        [SetUp]
        public void SetUp()
        {
            _concerts = new MemoryRepository<Concert>(x => x.Id);
            _concertDeletionService = new ConcertDeletionService(_concerts);
        }

        [Test]
        public void should_delete_concert()
        {
            var concert = _concerts.Add(new Concert());
            _concerts.Count(x => x.Id == concert.Id).ShouldEqual(1);

            _concertDeletionService.Delete(concert.Id);

            _concerts.Count(x => x.Id == concert.Id).ShouldEqual(0);
        }
    }
}