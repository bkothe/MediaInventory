using System.Collections.Generic;
using System.Data;
using MediaInventory.Core.Administration;
using MediaInventory.Core.Artist;
using MediaInventory.Core.Performance;
using MediaInventory.Core.Venue;
using MediaInventory.Infrastructure.Common.Data.Orm.NHibernate;

namespace MediaInventory.Tests.Common.Data.TestData
{
    public class Tracking
    {
        private readonly ILazySession _session;
        private readonly bool _inTransaction;
        private readonly Repositories _repositories;

        public Tracking(ILazySession session, Repositories repositories, bool inTransaction)
        {
            _session = session;
            _repositories = repositories;
            _inTransaction = inTransaction;

            if (_inTransaction)
                session.BeginTransaction(IsolationLevel.ReadCommitted);

            Users = new List<User>();
            Artists = new List<Artist>();
            Venues = new List<Venue>();
            Concerts = new List<Concert>();
        }

        public List<User> Users { get; }
        public List<Artist> Artists { get; }
        public List<Venue> Venues { get; }
        public List<Concert> Concerts { get; }

        public void CleanUp()
        {
            if (_inTransaction)
                _session.RollbackTransaction();
            else
                DeleteTrackedEntities();

            _session.Dispose();
        }

        private void DeleteTrackedEntities()
        {
            Users.ForEach(x => _repositories.UserRepository.Delete(x));
            Venues.ForEach(x => _repositories.VenueRepository.Delete(x));
            Concerts.ForEach(x => _repositories.ConcertRepository.Delete(x));
            Artists.ForEach(x => _repositories.ArtistRepository.Delete(x));

            _session.Flush();
        }
    }
}