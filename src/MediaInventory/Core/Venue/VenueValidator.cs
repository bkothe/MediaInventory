using System.Linq;
using FluentValidation;

namespace MediaInventory.Core.Venue
{
    public class VenueValidator : AbstractValidator<Venue>
    {
        public const string NameNullEmptyMessage = "Venue name must not be empty.";
        public const string NameLengthMessage = "The venue name must be between {MinLength} and {MaxLength} characters.";
        public const string NameDuplicateMessage = "The venue name '{PropertyValue}' already exists.";

        public const string StateNullEmptyMessage = "State must not be empty.";
        public const string StateLengthMessage = "The state must be {MinLength} characters.";

        public const string CityNullEmptyMessage = "City must not be empty.";
        public const string CityLengthMessage = "The city name must be between {MinLength} and {MaxLength} characters.";

        public VenueValidator(IQueryable<Venue> venues)
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(NameNullEmptyMessage)
                .Length(5, 50).WithMessage(NameLengthMessage)
                .Must((venue, name) => !venues.Any(x => x.Id != venue.Id && x.Name == name))
                    .WithMessage(NameDuplicateMessage);

            RuleFor(x => x.State)
                .NotEmpty().WithMessage(StateNullEmptyMessage)
                .Length(2).WithMessage(StateLengthMessage);

            RuleFor(x => x.City)
                .NotEmpty().WithMessage(StateNullEmptyMessage)
                .Length(2, 45).WithMessage(StateLengthMessage);
        }
    }
}
