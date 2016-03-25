using System.Linq;
using FluentValidation;

namespace MediaInventory.Core.Artist
{
    public class ArtistValidator : AbstractValidator<Artist>
    {
        public const string NameNullEmptyMessage = "Artist name must not be empty.";
        public const string NameLengthMessage = "Artist name must be between {MinLength} and {MaxLength} characters.  {TotalLength} characters were entered.";
        public const string NameDuplicateMessage = "The artist name '{PropertyValue}' already exists.";

        public ArtistValidator(IQueryable<Artist> artists)
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(NameNullEmptyMessage)
                .Length(2, 65).WithMessage(NameLengthMessage)
                .Must((artist, name) => !artists.Any(x => x.Id != artist.Id && x.Name == name))
                    .WithMessage(NameDuplicateMessage);
        }
    }
}