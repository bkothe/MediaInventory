using System;
using System.Collections.Generic;
using System.Linq;
using MediaInventory.Infrastructure.Common.Collections;
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

        public static DateTime ShouldNotBeWithinSeconds(this DateTime source, int seconds, bool utc = false)
        {
            return source.ShouldNotBeWithinSeconds(utc ? DateTime.UtcNow : DateTime.Now, seconds);
        }

        public static DateTime ShouldBeWithinSeconds(this DateTime source, int seconds, bool utc = false)
        {
            return source.ShouldBeWithinSeconds(utc ? DateTime.UtcNow : DateTime.Now, seconds);
        }

        public static DateTime ShouldNotBeWithinSeconds(this DateTime source, DateTime compare, int seconds)
        {
            source.ShouldNotBeInRange(compare.AddSeconds(-seconds), compare.AddSeconds(seconds));
            return source;
        }

        public static DateTime ShouldBeWithinSeconds(this DateTime source, DateTime compare, int seconds)
        {
            source.ShouldBeInRange(compare.AddSeconds(-seconds), compare.AddSeconds(seconds));
            return source;
        }

        public static DateTime? ShouldBeWithinSeconds(this DateTime? datetime, DateTime compare, int seconds)
        {
            datetime.HasValue.ShouldBeTrue();
            datetime.Value.ShouldBeWithinSeconds(compare, seconds);
            return datetime;
        }

        public static DateTime? ShouldBeWithinSeconds(this DateTime? datetime, DateTime? compare, int seconds)
        {
            datetime.HasValue.ShouldBeTrue();
            if (compare != null) datetime.Value.ShouldBeWithinSeconds(compare.Value, seconds);
            return datetime;
        }
    }
}
