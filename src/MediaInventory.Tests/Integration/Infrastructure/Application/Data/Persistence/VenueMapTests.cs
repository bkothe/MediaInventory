using System;
using FluentNHibernate.Testing;
using MediaInventory.Core.Venue;
using MediaInventory.Tests.Common.Comparers;
using MediaInventory.Tests.Common.Data.TestData;
using MediaInventory.Tests.Common.Extensions;
using NUnit.Framework;

namespace MediaInventory.Tests.Integration.Infrastructure.Application.Data.Persistence
{
    [TestFixture]
    public class VenueMapTests
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
            new PersistenceSpecification<Venue>(_testData.Session.GetSession())
                .CheckProperty(x => x.Name, RandomString.GenerateAlphaNumeric())
                .CheckProperty(x => x.City, RandomString.GenerateAlphaNumeric())
                .CheckProperty(x => x.State, RandomString.GenerateAlphaNumeric(2))
                .CheckProperty(x => x.Created, DateTime.Now, new DateTimeEqualityComparer(1))
                .CheckProperty(x => x.Modified, DateTime.Now, new DateTimeEqualityComparer(1))
                .CheckReference(x => x.PreviousVenue, new Venue
                    {
                        Name = RandomString.GenerateAlphaNumeric(),
                        City = RandomString.GenerateAlphaNumeric(),
                        State = RandomString.GenerateAlphaNumeric(2)
                    }, x => x.Id)
                .VerifyTheMappings();
        }

        [Test]
        public void should_correctly_persist_nulls()
        {
            new PersistenceSpecification<Venue>(_testData.Session.GetSession())
                .CheckProperty(x => x.Name, RandomString.GenerateAlphaNumeric())
                .CheckProperty(x => x.City, RandomString.GenerateAlphaNumeric())
                .CheckProperty(x => x.State, RandomString.GenerateAlphaNumeric(2))
                .CheckProperty(x => x.Created, DateTime.Now, new DateTimeEqualityComparer(1))
                .CheckProperty(x => x.Modified, null)
                .CheckReference(x => x.PreviousVenue, null)
                .VerifyTheMappings();
        }
    }
}