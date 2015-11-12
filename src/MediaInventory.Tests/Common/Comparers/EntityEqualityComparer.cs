using System;
using System.Collections;
using MediaInventory.Infrastructure.Common.Data.Orm;

namespace MediaInventory.Tests.Common.Comparers
{
    public class EntityEqualityComparer<T> : IEqualityComparer where T : IIdEntity
    {
        public new bool Equals(object x, object y)
        {
            if (x == null || y == null) return false;

            if (x is T && y is T) return ((IIdEntity)x).Id == ((IIdEntity)y).Id;

            return x.Equals(y);
        }

        public int GetHashCode(object obj)
        {
            throw new NotImplementedException();
        }
    }
}