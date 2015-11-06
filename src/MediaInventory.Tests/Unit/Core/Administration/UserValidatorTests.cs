using System;
using FluentValidation.TestHelper;
using MediaInventory.Core.Administration;
using MediaInventory.Tests.Common.Extensions;
using MediaInventory.Tests.Common.Fakes.Data;
using NUnit.Framework;
using Should;

namespace MediaInventory.Tests.Unit.Core.Administration
{
    [TestFixture]
    public class UserValidatorTests
    {
        private MemoryRepository<User> _users; 
        private UserValidator _userValidator;

        [SetUp]
        public void SetUp()
        {
            _users = new MemoryRepository<User>(x => x.Id);
            _userValidator = new UserValidator(_users);
        }

        [Test]
        public void should_not_have_error_for_valid_user()
        {
            _userValidator.Validate(new User
            {
                EmailAddress = RandomString.GenerateEmail()
            }).IsValid.ShouldBeTrue();
        }
        
        // email address
        [TestCase(null, TestName = "should_have_error_when_email_address_is_null")]
        [TestCase("", TestName = "should_have_error_when_email_address_is_zero_length")]
        [TestCase("       ", TestName = "should_have_error when_email_address_is_empty")]
        [TestCase("geddy.lee@.com")]
        [TestCase("geddy.lee@example")]
        [TestCase("geddy.leeexample.com")]
        public void should_have_error_when_email_address_is_invalid(string emailAddress)
        {
            _userValidator.ShouldHaveValidationErrorFor(x => x.EmailAddress, emailAddress);
        }

        [Test]
        public void should_not_have_error_when_email_address_is_valid()
        {
            _userValidator.ShouldNotHaveValidationErrorFor(x => x.EmailAddress, "geddy.lee@example.org");
        }

        [TestCase(5, TestName = "should_have_error_when_email_address_is_too_short")]
        [TestCase(255, TestName = "should_have_error_when_email_address_is_too_long")]
        public void should_have_error_for_email_address_length(int length)
        {
            _userValidator.ShouldHaveValidationErrorFor(x => x.EmailAddress, ($"{new string('x', length-4)}@x.l"));
        }

        [TestCase(6, TestName = "should_not_have_error_when_email_address_is_min_length")]
        [TestCase(254, TestName = "should_not_have_error_when_email_address_is_max_length")]
        public void should_not_have_error_for_email_address_length(int length)
        {
            _userValidator.ShouldNotHaveValidationErrorFor(x => x.EmailAddress, ($"{new string('x', length - 4)}@x.l"));
        }

        [Test]
        public void should_have_error_when_email_address_exists_for_different_user()
        {
            const string emailAddress = "geddy.lee@example.org";
            _users.Add(new User { EmailAddress = emailAddress });

            _userValidator.ShouldHaveValidationErrorFor(x => x.EmailAddress, emailAddress);
        }

        [Test]
        public void should_not_have_error_when_email_address_exists_with_for_same_user()
        {
            var user = _users.Add(new User { Id = Guid.NewGuid(), EmailAddress = "geddy.lee@example.org" });

            _userValidator.ShouldNotHaveValidationErrorFor(x => x.EmailAddress, user);
        }

        // first name
        [Test]
        public void should_have_error_when_first_name_is_too_long()
        {
            _userValidator.ShouldHaveValidationErrorFor(x => x.FirstName, new string('x', 26));
        }

        [TestCase(0, TestName = "should_not_have_error_when_first_name_is_min_length")]
        [TestCase(25, TestName = "should_not_have_error_when_first_name_is_max_length")]
        public void should_not_have_error_for_first_name_length(int length)
        {
            _userValidator.ShouldNotHaveValidationErrorFor(x => x.FirstName, new string('x', length));
        }

        // last name
        [Test]
        public void should_have_error_when_last_name_is_too_long()
        {
            _userValidator.ShouldHaveValidationErrorFor(x => x.LastName, new string('x', 26));
        }

        [TestCase(0, TestName = "should_not_have_error_when_last_name_is_min_length")]
        [TestCase(25, TestName = "should_not_have_error_when_last_name_is_max_length")]
        public void should_not_have_error_for_last_name_length(int length)
        {
            _userValidator.ShouldNotHaveValidationErrorFor(x => x.LastName, new string('x', length));
        }
    }
}