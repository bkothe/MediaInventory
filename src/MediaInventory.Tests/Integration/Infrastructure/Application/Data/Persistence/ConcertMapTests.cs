using System;
using FluentNHibernate.Testing;
using MediaInventory.Core.Performance;
using MediaInventory.Tests.Common.Comparers;
using MediaInventory.Tests.Common.Data.TestData;
using NUnit.Framework;

namespace MediaInventory.Tests.Integration.Infrastructure.Application.Data.Persistence
{
    [TestFixture]
    public class ConcertMapTests
    {
        private IntegrationTestData _testData;

        [SetUp]
        public void SetUp()
        {
            _testData = TestData.ForIntegrationTests();
        }

        [TearDown]
        public void TearDown()
        {
            _testData.CleanUp();
        }

        [Test]
        public void should_correctly_persist_entity()
        {
            new PersistenceSpecification<Concert>(_testData.Session.GetSession())
                .CheckProperty(x => x.Date, DateTime.Now, new DateTimeEqualityComparer(1))
                .CheckProperty(x => x.Created, DateTime.Now, new DateTimeEqualityComparer(1))
                .CheckProperty(x => x.Modified, DateTime.Now, new DateTimeEqualityComparer(1))
                .CheckReference(x => x.Artist, _testData.Artists.Create().Artist, x => x.Id)
                .CheckReference(x => x.Venue, _testData.Venues.Create().Venue, x => x.Id)
                .VerifyTheMappings();
        }
    }
}