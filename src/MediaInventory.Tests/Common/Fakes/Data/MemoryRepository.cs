using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using MediaInventory.Infrastructure.Framework.Data.Orm;

namespace MediaInventory.Tests.Common.Fakes.Data
{
    public class MemoryRepository<TEntity> : IRepository<TEntity>, IList<TEntity> where TEntity : class, new()
    {
        private readonly IList<TEntity> _entities;
        private readonly Func<TEntity, object> _key;
        private readonly PropertyInfo _keyProperty;

        public MemoryRepository(Expression<Func<TEntity, object>> key, params TEntity[] entities)
        {
            _entities = entities.ToList();
            _key = key.Compile();
            _keyProperty = (PropertyInfo)((MemberExpression)(key.Body is MemberExpression ? key.Body : ((UnaryExpression)key.Body).Operand)).Member;
        }

        public TEntity Get<T>(T id) where T : struct
        {
            return _entities.FirstOrDefault(x => _key(x).Equals(id));
        }

        public TEntity Get<T>(T id, bool lazy) where T : struct
        {
            return lazy ? Get(id) ?? GetLazy(id) : Get(id);
        }

        private TEntity GetLazy<T>(T id)
        {
            var entity = new TEntity();
            _keyProperty.SetValue(entity, id, null);
            return entity;
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            return _entities.FirstOrDefault(filter.Compile());
        }

        public TEntity Add(TEntity entity)
        {
            var id = _keyProperty.GetValue(entity, null);
            if (_keyProperty.PropertyType == typeof(Guid) && ((Guid)id) == Guid.Empty)
                _keyProperty.SetValue(entity, Guid.NewGuid(), null);
            else if (_keyProperty.PropertyType == typeof(int) && ((int)id) == 0)
                _keyProperty.SetValue(entity, _entities.Any() ? (int)this.Max(_key) + 1 : 1, null);
            _entities.Add(entity);
            return entity;
        }

        public void Modify(TEntity entity) { }

        public void Delete<T>(T id) where T : struct
        {
            var entity = _entities.FirstOrDefault(x => _key(x).Equals(id));
            if (entity != null) _entities.Remove(entity);
        }

        public void Delete(Expression<Func<TEntity, bool>> filter)
        {
            var entity = _entities.FirstOrDefault(filter.Compile());
            if (entity != null) _entities.Remove(entity);
        }

        public void Delete(TEntity entity)
        {
            _entities.Remove(entity);
        }

        public int DeleteMany(Expression<Func<TEntity, bool>> filter)
        {
            return DeleteMany(this.Where(filter.Compile()).AsQueryable());
        }

        public int DeleteMany(IQueryable<TEntity> source)
        {
            var entities = source.ToList();
            if (entities.Any()) entities.ToList().ForEach(x => _entities.Remove(x));
            return entities.Count;
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return _entities.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _entities.GetEnumerator();
        }

        public Type ElementType { get { return _entities.AsQueryable().ElementType; } }
        public Expression Expression { get { return _entities.AsQueryable().Expression; } }
        public IQueryProvider Provider { get { return _entities.AsQueryable().Provider; } }

        public int Count { get { return _entities.Count; } }
        public bool IsReadOnly { get { return _entities.IsReadOnly; } }

        public void Clear()
        {
            _entities.Clear();
        }

        public bool Contains(TEntity item)
        {
            return _entities.Contains(item);
        }

        public void CopyTo(TEntity[] array, int arrayIndex)
        {
            _entities.CopyTo(array, arrayIndex);
        }

        public bool Remove(TEntity item)
        {
            return _entities.Remove(item);
        }

        void ICollection<TEntity>.Add(TEntity item)
        {
            _entities.Add(item);
        }

        public int IndexOf(TEntity item)
        {
            return _entities.IndexOf(item);
        }

        public void Insert(int index, TEntity item)
        {
            _entities.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _entities.RemoveAt(index);
        }

        public TEntity this[int index]
        {
            get { return _entities[index]; }
            set { _entities[index] = value; }
        }
    }
}