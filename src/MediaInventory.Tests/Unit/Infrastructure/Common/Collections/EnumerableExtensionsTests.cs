using MediaInventory.Infrastructure.Common.Collections;
using NUnit.Framework;
using Should;

namespace MediaInventory.Tests.Unit.Infrastructure.Common.Collections
{
    [TestFixture]
    public class EnumerableExtensionsTests
    {
        // Aggregate
        [Test]
        public void should_aggregate()
        {
            new[] { 1, 2, 3 }.Aggregate(", ").ShouldEqual("1, 2, 3");
        }

        // AreEquivalent
        [Test]
        public void should_be_equivalent_when_ordered()
        {
            new[] { 1, 2, 3 }.AreEquivalent(new[] { 1, 2, 3 }).ShouldBeTrue();
        }

        [Test]
        public void should_be_equivalent_when_unordered()
        {
            new[] { 1, 2, 3 }.AreEquivalent(new[] { 3, 2, 1 }).ShouldBeTrue();
        }

        [Test]
        public void should_not_be_equivalent_when_first_contains_extra()
        {
            new[] { 1, 2, 3, 4 }.AreEquivalent(new[] { 3, 2, 1 }).ShouldBeFalse();
        }

        [Test]
        public void should_not_be_equivalent_when_second_contains_extra()
        {
            new[] { 1, 2, 3 }.AreEquivalent(new[] { 3, 2, 1, 4 }).ShouldBeFalse();
        }

        [Test]
        public void should_not_be_equivalent_when_first_contains_duplicates()
        {
            new[] { 1, 2, 3, 2 }.AreEquivalent(new[] { 3, 2, 1 }).ShouldBeFalse();
        }

        [Test]
        public void should_not_be_equivalent_when_second_contains_duplicates()
        {
            new[] { 1, 2, 3 }.AreEquivalent(new[] { 3, 2, 1, 3 }).ShouldBeFalse();
        }
    }
}