using System;
using NHibernate;

namespace MediaInventory.Infrastructure.Framework.Data.Orm.NHibernate
{
    public interface ILazySessionFactory : IDisposable
    {
        ISession OpenSession();
    }

    public class LazySessionFactory : ILazySessionFactory
    {
        private readonly Lazy<ISessionFactory> _factory;

        public LazySessionFactory(ISessionFactoryBuilder factory)
        {
            _factory = new Lazy<ISessionFactory>(factory.Build);
        }

        public ISession OpenSession()
        {
            return _factory.Value.OpenSession();
        }

        public void Dispose()
        {
            if (_factory.IsValueCreated)
                _factory.Value.Dispose();
        }
    }
}