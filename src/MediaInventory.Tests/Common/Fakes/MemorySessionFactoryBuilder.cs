using MediaInventory.Infrastructure.Framework.Data.Orm.NHibernate;
using NHibernate;

namespace MediaInventory.Tests.Common.Fakes
{
    public class MemorySessionFactoryBuilder : ISessionFactoryBuilder
    {
        private readonly ISessionFactory _sessionFactory;

        public MemorySessionFactoryBuilder(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public ISessionFactory Build()
        {
            return _sessionFactory;
        }
    }
}