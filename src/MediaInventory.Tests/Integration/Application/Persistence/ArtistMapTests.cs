using System;
using FluentNHibernate.Testing;
using MediaInventory.Core.Artist;
using MediaInventory.Tests.Common.Comparers;
using MediaInventory.Tests.Common.Data.TestData;
using MediaInventory.Tests.Common.Extensions;
using NUnit.Framework;

namespace MediaInventory.Tests.Integration.Application.Persistence
{
    [TestFixture]
    public class ArtistMapTests
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
            new PersistenceSpecification<Artist>(_testData.Session.GetSession())
                .CheckProperty(x => x.Name, RandomString.GenerateAlphaNumeric())
                .CheckProperty(x => x.Created, DateTime.Now, new DateTimeEqualityComparer(1))
                .CheckProperty(x => x.Modified, DateTime.Now, new DateTimeEqualityComparer(1))
                .VerifyTheMappings();
        }
    }
}
