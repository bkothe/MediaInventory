using System;
using System.Linq;
using System.Linq.Expressions;

namespace MediaInventory.Infrastructure.Common.Data.Orm
{
    public interface IRepository<TEntity> : IQueryable<TEntity> where TEntity : class, new()
    {
        TEntity Get<T>(T id) where T : struct;
        TEntity Get<T>(T id, bool lazy) where T : struct;
        TEntity Get(Expression<Func<TEntity, bool>> filter);
        TEntity Add(TEntity entity);
        void Modify(TEntity entity);
        void Delete(TEntity entity);
        void Delete<T>(T id) where T : struct;
        void Delete(Expression<Func<TEntity, bool>> filter);
        int DeleteMany(Expression<Func<TEntity, bool>> filter);
        int DeleteMany(IQueryable<TEntity> source);
    }
}