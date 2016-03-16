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
            ArtistRepository = new Repository<Artist>(session);
            CommercialAudioMediaRepository = new Repository<CommercialAudioMedia>(session);
            ConcertRepository = new Repository<Concert>(session);
            UserRepository = new Repository<User>(session);
            VenueRepository = new Repository<Venue>(session);
        }

        public IRepository<Artist> ArtistRepository { get; }
        public IRepository<CommercialAudioMedia> CommercialAudioMediaRepository { get; }
        public IRepository<Concert> ConcertRepository { get; }
        public IRepository<User> UserRepository { get; }
        public IRepository<Venue> VenueRepository { get; }
    }
}