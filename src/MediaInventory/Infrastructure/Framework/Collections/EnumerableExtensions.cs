using System.Collections.Generic;
using System.Linq;

namespace MediaInventory.Infrastructure.Framework.Collections
{
    public static class EnumerableExtensions
    {
        public static string Aggregate<T>(this IEnumerable<T> source, string delimiter)
        {
            return source.Select(x => x.ToString()).Aggregate((a, i) => a + delimiter + i);
        }

        public static bool AreEquivalent<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            var secondList = second.Cast<object>().ToList();
            foreach (var index in first.Select(item => secondList.FindIndex(item.Equals)))
            {
                if (index < 0)
                    return false;

                secondList.RemoveAt(index);
            }
            return secondList.Count == 0;
        }
    }
}