using System.Collections.Generic;
using System.Data;
using MediaInventory.Core.Administration;
using MediaInventory.Core.Artist;
using MediaInventory.Core.Media;
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

            Artists = new List<Artist>();
            Audios = new List<Audio>();
            Concerts = new List<Concert>();
            Users = new List<User>();
            Venues = new List<Venue>();
        }

        public List<Artist> Artists { get; }
        public List<Audio> Audios { get; }
        public List<Concert> Concerts { get; }
        public List<User> Users { get; }
        public List<Venue> Venues { get; }

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
            Audios.ForEach(x => _repositories.Audios.Delete(x));
            Concerts.ForEach(x => _repositories.Concerts.Delete(x));
            Artists.ForEach(x => _repositories.Artists.Delete(x));
            Venues.ForEach(x => _repositories.Venues.Delete(x));
            Users.ForEach(x => _repositories.Users.Delete(x));

            _session.Flush();
        }
    }
}