using System.Linq;
using FluentValidation;

namespace MediaInventory.Core.Administration
{
    public class UserValidator : AbstractValidator<User>
    {
        public const string EmailAddressNullEmptyMessage = "Email address can not be empty.";
        public const string EmailAddressLengthMessage = "Email address must be between {MinLength} and {MaxLength} characters long.";
        public const string EmailAddressExistsMessage = "The email address '{PropertyValue}' already exists.";
        public const string EmailAddressInvalidMessage = "The email address '{PropertyValue}' is invalid.";

        public const string FirstNameLengthMessage = "First name must be between {MinLength} and {MaxLength} characters long.";

        public const string LastNameLengthMessage = "Last name must be between {MinLength} and {MaxLength} characters long.";

        public UserValidator(IQueryable<User> users)
        {
            RuleFor(x => x.EmailAddress)
                .NotEmpty().WithMessage(EmailAddressNullEmptyMessage)
                .Length(6, 254).WithMessage(EmailAddressLengthMessage)
                .Must((user, emailAddress) => !users.Any(x => x.EmailAddress == emailAddress && x.Id != user.Id))
                    .WithMessage(EmailAddressExistsMessage)
                .EmailAddress().WithMessage(EmailAddressInvalidMessage);

            RuleFor(x => x.FirstName)
                .Length(0, 25).WithMessage(FirstNameLengthMessage);

            RuleFor(x => x.LastName)
                .Length(0, 25).WithMessage(LastNameLengthMessage);
        }
    }
}