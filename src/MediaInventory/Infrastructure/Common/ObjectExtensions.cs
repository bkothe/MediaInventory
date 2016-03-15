using System;

namespace MediaInventory.Infrastructure.Common
{
    public static class ObjectExtensions
    {
        public static bool ActOn<T>(this T value, Func<T, bool> predicate, Action<T> action)
        {
            if (!predicate(value)) return false;
            action(value);
            return true;
        }

        public static T ActOn<T>(this T value, Action<T> action)
        {
            action?.Invoke(value);
            return value;
        }

        public static T ThenDo<T>(this T result, Action action)
        {
            action();
            return result;
        }

        public static bool IsNull(this object value)
        {
            return value == null || value == DBNull.Value;
        }

        public static bool IsNotNull(this object value)
        {
            return !value.IsNull();
        }
    }
}