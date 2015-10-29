using System;
using MediaInventory.Infrastructure.Framework.Exceptions;

namespace MediaInventory.Infrastructure.Framework.Reflection
{
    public static class ObjectExtensions
    {
        public static TEntity IfNullThrowNotFound<TEntity>(this TEntity source, Guid entityId, string name)
        {
            if (source.IsNull()) throw new NotFoundException(entityId, name);

            return source;
        }
    }
}