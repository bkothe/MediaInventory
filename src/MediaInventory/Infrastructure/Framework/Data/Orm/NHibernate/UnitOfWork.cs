using System.Data;

namespace MediaInventory.Infrastructure.Framework.Data.Orm.NHibernate
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ILazySession _session;

        public UnitOfWork(ILazySession session)
        {
            _session = session;
        }

        public void Begin()
        {
            _session.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public void Begin(IsolationLevel isolationLevel)
        {
            _session.BeginTransaction(isolationLevel);
        }

        public void Commit()
        {
            _session.CommitTransaction();
        }

        public void Rollback()
        {
            _session.RollbackTransaction();
        }

        public void Flush()
        {
            _session.Flush();
        }

        public void Dispose()
        {
            _session.Dispose();
        }

        public void Reload<T>(T entity)
        {
            _session.Refresh(entity);
        }
    }
}