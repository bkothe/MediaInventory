using MediaInventory.Core.Artist;
using MediaInventory.Core.Media;
using MediaInventory.Core.Performance;
using MediaInventory.Core.Venue;
using MediaInventory.Infrastructure.Common.Data.Orm.NHibernate;

namespace MediaInventory.Tests.Common.Data.TestData
{
    public abstract class TestData
    {
        private readonly Context _context;

        protected TestData(Context context)
        {
            _context = context;
            Artists = new ArtistsDsl(_context);
            Audios = new AudiosDsl(_context);
            Concerts = new ConcertsDsl(_context);
            Venues = new VenuesDsl(_context);
        }

        public static IntegrationTestData ForIntegrationTests()
        {
            return new IntegrationTestData(CreateContext(true));
        }

        public static AcceptanceTestData ForAcceptanceTests()
        {
            return new AcceptanceTestData(CreateContext(false));
        }

        private static Context CreateContext(bool inTransaction)
        {
            var session = NHibernate.OpenMediaInventorySession();
            var repositories = new Repositories(session);
            var tracking = new Tracking(session, repositories, inTransaction);
            return new Context(session, repositories, tracking);
        }

        // NHibernate
        public ILazySession Session => _context.Session;
        public Repositories Repositories => _context.Repositories;

        public T Refresh<T>(T entity)
        {
            Session.Refresh(entity);
            return entity;
        }

        public T FlushAndRefresh<T>(T entity)
        {
            Session.Flush();
            return Refresh(entity);
        }

        // Data generation
        public ArtistsDsl Artists { get; private set; }
        public ConcertsDsl Concerts { get; private set; }
        public AudiosDsl Audios { get; private set; }
        public VenuesDsl Venues { get; private set; }

        // Tracking
        public Tracking Tracking => _context.Tracking;

        public Artist IncludeInCleanUp(Artist entity) { Tracking.Artists.Add(entity); return entity; }
        public Audio IncludeInCleanUp(Audio entity) { Tracking.Audios.Add(entity); return entity; }
        public Concert IncludeInCleanUp(Concert entity) { Tracking.Concerts.Add(entity); return entity; }
        public Venue IncludeInCleanUp(Venue entity) { Tracking.Venues.Add(entity); return entity; }

        public void ExcludeInCleanup(Artist entity) { Tracking.Artists.Remove(entity); }
        public void ExcludeInCleanup(Audio entity) { Tracking.Audios.Remove(entity); }
        public void ExcludeInCleanup(Concert entity) { Tracking.Concerts.Remove(entity); }
        public void ExcludeInCleanup(Venue entity) { Tracking.Venues.Remove(entity); }

        public void CleanUp()
        {
            Tracking.CleanUp();
        }
    }

    public class IntegrationTestData : TestData
    {
        private readonly Context _context;

        public IntegrationTestData(Context context)
            : base(context)
        {
            _context = context;
        }
    }

    public class AcceptanceTestData : TestData
    {
        private readonly Context _context;

        public AcceptanceTestData(Context context) : base(context)
        {
            _context = context;
        }

        public UiAcceptanceTestData OnUi()
        {
            return new UiAcceptanceTestData(_context);
        }
    }

    public class UiAcceptanceTestData : TestData
    {
        private readonly Context _context;

        public UiAcceptanceTestData(Context context)
            : base(context)
        {
            _context = context;
        }
    }
}