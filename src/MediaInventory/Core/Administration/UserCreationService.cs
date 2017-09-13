using MediaInventory.Infrastructure.Common.Data.Orm;
using MediaInventory.Infrastructure.Common.Exceptions;

namespace MediaInventory.Core.Administration
{
    public interface IUserCreationService
    {
        User Create(string emailAddress);
    }

    public class UserCreationService : IUserCreationService
    {
        private readonly IRepository<User> _users;
        private readonly UserValidator _userValidator;

        public UserCreationService(IRepository<User> users, UserValidator userValidator)
        {
            _users = users;
            _userValidator = userValidator;
        }

        public User Create(string emailAddress)
        {
            var user = new User { EmailAddress = emailAddress };

            _userValidator.ValidateAndThrow(user);

            return _users.Add(user);
        }
    }
}