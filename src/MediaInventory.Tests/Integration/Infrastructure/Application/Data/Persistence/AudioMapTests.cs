using System;
using FluentNHibernate.Testing;
using MediaInventory.Core.Media;
using MediaInventory.Tests.Common.Comparers;
using MediaInventory.Tests.Common.Data.TestData;
using MediaInventory.Tests.Common.Extensions;
using NUnit.Framework;

namespace MediaInventory.Tests.Integration.Infrastructure.Application.Data.Persistence
{
    [TestFixture]
    class AudioMapTests
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
            new PersistenceSpecification<Audio>(_testData.Session.GetSession())
                .CheckProperty(x => x.Title, RandomString.GenerateAlphaNumeric())
                .CheckProperty(x => x.MediaFormat, MediaFormat.Vinyl)
                .CheckProperty(x => x.Released, DateTime.Now, new DateTimeEqualityComparer(1))
                .CheckProperty(x => x.Purchased, DateTime.Now, new DateTimeEqualityComparer(1))
                .CheckProperty(x => x.PurchasePrice, 10.45M)
                .CheckProperty(x => x.PurchaseLocation, RandomString.GenerateAlphaNumeric())
                .CheckProperty(x => x.MediaCount, 3)
                .CheckProperty(x => x.Notes, RandomString.GenerateAlphaNumeric())
                .CheckProperty(x => x.Created, DateTime.Now, new DateTimeEqualityComparer(1))
                .CheckProperty(x => x.Modified, DateTime.Now, new DateTimeEqualityComparer(1))
                .CheckReference(x => x.Artist, _testData.Artists.Create().Artist, x => x.Id)
                .VerifyTheMappings();
        }

        [Test]
        public void should_persist_nullables()
        {
            new PersistenceSpecification<Audio>(_testData.Session.GetSession())
                .CheckProperty(x => x.Title, RandomString.GenerateAlphaNumeric())
                .CheckProperty(x => x.MediaFormat, MediaFormat.Vinyl)
                .CheckProperty(x => x.Released, null)
                .CheckProperty(x => x.Purchased, null)
                .CheckProperty(x => x.PurchasePrice, null)
                .CheckProperty(x => x.PurchaseLocation, null)
                .CheckProperty(x => x.MediaCount, 3)
                .CheckProperty(x => x.Notes, null)
                .CheckProperty(x => x.Created, DateTime.Now, new DateTimeEqualityComparer(1))
                .CheckProperty(x => x.Modified, null)
                .CheckReference(x => x.Artist, _testData.Artists.Create().Artist, x => x.Id)
                .VerifyTheMappings();
        }
    }
}