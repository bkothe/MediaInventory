using MediaInventory.Infrastructure.Common.Data.Orm.NHibernate;

namespace MediaInventory.Tests.Common.Data.TestData
{
    public abstract class TestData
    {
        private readonly Context _context;

        protected TestData(Context context)
        {
            _context = context;
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

        // Tracking
        public Tracking Tracking => _context.Tracking;

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
    }
}