using MediaInventory.Core.Administration;
using MediaInventory.Core.Artist;
using MediaInventory.Core.Media;
using MediaInventory.Core.Performance;
using MediaInventory.Core.Venue;
using MediaInventory.Infrastructure.Common.Data.Orm;
using MediaInventory.Infrastructure.Common.Data.Orm.NHibernate;

namespace MediaInventory.Tests.Common.Data.TestData
{
    public class Repositories
    {
        public Repositories(ILazySession session)
        {
            Artists = new Repository<Artist>(session);
            Audios = new Repository<Audio>(session);
            Concerts = new Repository<Concert>(session);
            Users = new Repository<User>(session);
            Venues = new Repository<Venue>(session);
        }

        public IRepository<Artist> Artists { get; }
        public IRepository<Audio> Audios { get; }
        public IRepository<Concert> Concerts { get; }
        public IRepository<User> Users { get; }
        public IRepository<Venue> Venues { get; }
    }
}