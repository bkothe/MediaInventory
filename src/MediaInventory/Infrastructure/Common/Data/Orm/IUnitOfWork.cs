using System;
using System.Data;

namespace MediaInventory.Infrastructure.Common.Data.Orm
{
    public interface IUnitOfWork : IDisposable
    {
        void Begin();
        void Begin(IsolationLevel isolationLevel);
        void Commit();
        void Rollback();
        void Flush();
        void Reload<T>(T entity);
    }
}