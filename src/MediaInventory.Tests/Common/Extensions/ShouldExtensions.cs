using MediaInventory.Infrastructure.Framework.Collections;
using System.Collections.Generic;
using System.Linq;
using Should;

namespace MediaInventory.Tests.Common.Extensions
{
    public static class ShouldExtensions
    {
        public static void ShouldBeEquivalent<T>(this IEnumerable<T> source, IEnumerable<T> expected)
        {
            var sourceArray = source as T[] ?? source.ToArray();
            var expectedArray = expected as T[] ?? expected.ToArray();
            sourceArray.AreEquivalent(expectedArray)
                .ShouldBeTrue(string.Format("Expected: equivalent to <{0}>, But was: <{1}>", expectedArray.Aggregate(","), sourceArray.Aggregate(",")));
        }
    }
}
