using System;
using MediaInventory.Infrastructure.Common.Exceptions;

namespace MediaInventory.Infrastructure.Common.Reflection
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