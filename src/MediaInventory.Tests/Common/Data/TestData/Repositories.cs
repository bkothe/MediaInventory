using MediaInventory.Core.Artist;
using MediaInventory.Core.Performance;
using MediaInventory.Core.Venue;
using MediaInventory.Infrastructure.Framework.Data.Orm;
using MediaInventory.Infrastructure.Framework.Data.Orm.NHibernate;

namespace MediaInventory.Tests.Common.Data.TestData
{
    public class Repositories
    {
        public Repositories(ILazySession session)
        {
            ArtistRepository = new Repository<Artist>(session);
            VenueRepository = new Repository<Venue>(session);
            ConcertRepository = new Repository<Concert>(session);
        }

        public IRepository<Artist> ArtistRepository { get; }
        public IRepository<Venue> VenueRepository { get; }
        public IRepository<Concert> ConcertRepository { get; }
    }
}