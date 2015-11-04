using System;
using System.Data;

namespace MediaInventory.Infrastructure.Framework.Data.Orm
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