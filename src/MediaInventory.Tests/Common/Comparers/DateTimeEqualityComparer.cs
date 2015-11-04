using System;
using System.Collections;

namespace MediaInventory.Tests.Common.Comparers
{
    class DateTimeEqualityComparer : IEqualityComparer
    {
        private readonly TimeSpan _maxDifference;

        public DateTimeEqualityComparer(int seconds)
        {
            _maxDifference = TimeSpan.FromSeconds(seconds);
        }

        public DateTimeEqualityComparer(TimeSpan maxDifference)
        {
            _maxDifference = maxDifference;
        }

        public bool Equals(object x, object y)
        {
            if (x == null || y == null)
                return false;

            if (x is DateTime && y is DateTime)
                return ((DateTime)x - (DateTime)y).Duration() < _maxDifference;

            return x.Equals(y);
        }

        public int GetHashCode(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
