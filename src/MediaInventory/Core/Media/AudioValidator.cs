using System.Linq;
using FluentValidation;
using MediaInventory.Infrastructure.Common.Data.Orm;

namespace MediaInventory.Core.Media
{
    public class AudioValidator : AbstractValidator<Audio>
    {
        public const string ArtistNullMessage = "An artist is required.";
        public const string ArtistNotExistsMessage = "The artist does not exist.";
        public const string TitleNotNullEmptyMessage = "A title is required";
        public const string TitleLengthMessage = "Title must be between {MinLength} and {MaxLength} characters.  {TotalLength} characters were entered.";
        public const string MediaFormatNullMessage = "Media format is required.";
        public const string MediaCountNotNullMessage = "Media count is required";
        public const string MediaCountGreaterThanMessage = "Media count must be greater than {ComparisonValue}.";

        public AudioValidator(IRepository<Artist.Artist> artists)
        {
            RuleFor(x => x.Artist)
                .NotNull().WithMessage(ArtistNullMessage)
                .Must(artist => artists.Any(y => y.Id == artist.Id))
                    .WithMessage(ArtistNotExistsMessage);

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage(TitleNotNullEmptyMessage)
                .Length(1, 85).WithMessage(TitleLengthMessage);

            RuleFor(x => x.MediaFormat)
                .NotNull().WithMessage(MediaFormatNullMessage);

            RuleFor(x => x.MediaCount)
                .NotNull().WithMessage(MediaCountNotNullMessage)
                .GreaterThan(0).WithMessage(MediaCountGreaterThanMessage);
        }
    }
}