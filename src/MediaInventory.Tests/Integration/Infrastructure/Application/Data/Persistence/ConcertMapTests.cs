using System;
using FluentNHibernate.Testing;
using MediaInventory.Core.Artist;
using MediaInventory.Core.Performance;
using MediaInventory.Core.Venue;
using MediaInventory.Tests.Common.Comparers;
using MediaInventory.Tests.Common.Data.TestData;
using MediaInventory.Tests.Common.Extensions;
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
                .CheckReference(x => x.Artist, new Artist
                {
                    Name = RandomString.GenerateAlphaNumeric()
                }, x => x.Id)
                .CheckReference(x => x.Venue, new Venue
                {
                    Name = RandomString.GenerateAlphaNumeric(),
                    City = RandomString.GenerateAlphaNumeric(),
                    State = RandomString.GenerateAlphaNumeric(2)
                }, x => x.Id)
                .VerifyTheMappings();
        }

        [Test]
        public void should_correctly_persist_entity_with_existing_references()
        {
            var artist = _testData.Repositories.ArtistRepository.Add(new Artist { Name = RandomString.GenerateAlphaNumeric() });
            var venue = _testData.Repositories.VenueRepository.Add(new Venue
            {
                Name = RandomString.GenerateAlphaNumeric(),
                City = RandomString.GenerateAlphaNumeric(),
                State = RandomString.GenerateAlphaNumeric(2)
            });

            _testData.Session.Flush();

            new PersistenceSpecification<Concert>(_testData.Session.GetSession())
                .CheckProperty(x => x.Date, DateTime.Now, new DateTimeEqualityComparer(1))
                .CheckProperty(x => x.Created, DateTime.Now, new DateTimeEqualityComparer(1))
                .CheckProperty(x => x.Modified, DateTime.Now, new DateTimeEqualityComparer(1))
                .CheckReference(x => x.Artist, artist, x => x.Id)
                .CheckReference(x => x.Venue, venue, x => x.Id)
                .VerifyTheMappings();
        }
    }
}