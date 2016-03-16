using System;
using System.Linq;
using MediaInventory.Infrastructure.Common.Exceptions;

namespace MediaInventory.Infrastructure.Common.Collections
{
    public static class QueryableExtensions
    {
        public static T FirstOrThrowNotFound<T>(this IQueryable<T> source, Func<T, bool> where, object key, string name)
        {
            var entity = source.Where(where).FirstOrDefault();

            if (entity.IsNull())
                throw new NotFoundException(key, name);

            return entity;
        }

        public static T FirstOrThrowNotFound<T>(this IQueryable<T> source, Func<T, bool> where)
        {
            var entity = source.Where(where).FirstOrDefault();

            if (entity.IsNull())
                throw new NotFoundException(null, typeof(T).ToString());

            return entity;
        }
    }
}