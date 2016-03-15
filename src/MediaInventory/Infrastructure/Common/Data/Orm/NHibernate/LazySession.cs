using System;
using System.Data;
using NHibernate;

namespace MediaInventory.Infrastructure.Common.Data.Orm.NHibernate
{
    public interface ILazySession : IDisposable
    {
        ISession GetSession();
        void BeginTransaction(IsolationLevel isolationLevel);
        void CommitTransaction();
        void RollbackTransaction();
        void Refresh(object entity);
        void Flush();
    }

    public class LazySession : ILazySession
    {
        private readonly Lazy<ISession> _session;
        private ITransaction _transaction;
        private IsolationLevel? _lazyTransaction;

        public LazySession(ILazySessionFactory sessionFactory)
        {
            _session = new Lazy<ISession>(sessionFactory.OpenSession);
        }

        public ISession GetSession()
        {
            if (!_session.IsValueCreated && _lazyTransaction.HasValue)
                _transaction = _session.Value.BeginTransaction(_lazyTransaction.Value);
            return _session.Value;
        }

        public void BeginTransaction(IsolationLevel isolationLevel)
        {
            if (_session.IsValueCreated)
                _transaction =
                    _session.Value.BeginTransaction(isolationLevel);
            else _lazyTransaction = isolationLevel;
        }

        public void CommitTransaction()
        {
            if (_transaction == null || !_transaction.IsActive) return;
            _transaction.Commit();
            _transaction.Dispose();
        }

        public void RollbackTransaction()
        {
            if (_transaction == null || !_transaction.IsActive) return;
            // We don't rollback exceptions to obscure the exception
            // that triggered the rollback in the first place.
            try
            {
                _transaction.Rollback();
                _transaction.Dispose();
            }
            catch { }
        }

        public void Refresh(object entity)
        {
            if (_session.IsValueCreated) _session.Value.Refresh(entity);
        }

        public void Flush()
        {
            if (_session.IsValueCreated) _session.Value.Flush();
        }

        public void Dispose()
        {
            if (_session.IsValueCreated) _session.Value.Dispose();
        }
    }
}