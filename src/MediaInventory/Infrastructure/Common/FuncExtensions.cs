using System;
using System.Collections.Concurrent;

namespace MediaInventory.Infrastructure.Common
{
    public static class FuncExtensions
    {
        public static Func<T1, T2> Memoize<T1, T2>(this Func<T1, T2> func)
        {
            var map = new ConcurrentDictionary<T1, T2>();
            return x =>
            {
                if (map.ContainsKey(x)) return map[x];
                var result = func(x);
                map.TryAdd(x, result);
                return result;
            };
        }

        public static Func<T1, T2, T3> Memoize<T1, T2, T3>(this Func<T1, T2, T3> func)
        {
            var map = new ConcurrentDictionary<T1, T3>();
            return (x, y) =>
            {
                if (map.ContainsKey(x)) return map[x];
                var result = func(x, y);
                map.TryAdd(x, result);
                return result;
            };
        }
    }
}