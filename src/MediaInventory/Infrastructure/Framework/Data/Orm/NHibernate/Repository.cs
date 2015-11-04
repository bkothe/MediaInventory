using System;
using System.Linq;
using System.Linq.Expressions;
using NHibernate.Linq;

namespace MediaInventory.Infrastructure.Framework.Data.Orm.NHibernate
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        private readonly ILazySession _session;

        public Repository(ILazySession session)
        {
            _session = session;
        }

        public TEntity Get<T>(T id) where T : struct { return Get(id, false); }

        public TEntity Get<T>(T id, bool lazy) where T : struct
        {
            return lazy ? _session.GetSession().Load<TEntity>(id) : _session.GetSession().Get<TEntity>(id);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            return _session.GetSession().Query<TEntity>().FirstOrDefault(filter);
        }

        public TEntity Add(TEntity entity)
        {
            _session.GetSession().Save(entity);
            return entity;
        }

        public void Modify(TEntity entity)
        {
            _session.GetSession().Update(entity);
        }

        public void Delete(Expression<Func<TEntity, bool>> filter)
        {
            var entity = _session.GetSession().Query<TEntity>().FirstOrDefault(filter);
            if (entity != null) Delete(entity);
        }

        public void Delete<T>(T id) where T : struct
        {
            _session.GetSession().Delete(_session.GetSession().Load<TEntity>(id));
        }

        public void Delete(TEntity entity)
        {
            _session.GetSession().Delete(entity);
        }

        public int DeleteMany(Expression<Func<TEntity, bool>> filter)
        {
            var results = _session.GetSession().Query<TEntity>().Where(filter).ToList();
            results.ForEach(x => _session.GetSession().Delete(x));
            return results.Count;
        }

        public int DeleteMany(IQueryable<TEntity> source)
        {
            var results = source.ToList();
            results.ForEach(x => _session.GetSession().Delete(x));
            return results.Count;
        }

        public System.Collections.Generic.IEnumerator<TEntity> GetEnumerator()
        {
            return _session.GetSession().Query<TEntity>().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _session.GetSession().Query<TEntity>().GetEnumerator();
        }

        public Type ElementType { get { return _session.GetSession().Query<TEntity>().ElementType; } }
        public Expression Expression { get { return _session.GetSession().Query<TEntity>().Expression; } }
        public IQueryProvider Provider { get { return _session.GetSession().Query<TEntity>().Provider; } }
    }
}
