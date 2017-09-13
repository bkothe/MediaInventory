using MediaInventory.Core.Administration;
using MediaInventory.Tests.Common.Fakes.Data;
using NUnit.Framework;
using Should;

namespace MediaInventory.Tests.Unit.Core.Administration
{
    [TestFixture]
    public class UserCreationServiceTests
    {
        private MemoryRepository<User> _users;
        private UserCreationService _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new UserCreationService(_users, new UserValidator(_users));
        }

        [Test]
        public void should()
        {

        }
    }
}