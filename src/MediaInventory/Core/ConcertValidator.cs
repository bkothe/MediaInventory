using System.Linq;
using FluentValidation;

namespace MediaInventory.Core
{
    public class ConcertValidator : AbstractValidator<Concert>
    {
        public const string ArtistNullMessage = "An artist is required.";
        public const string ArtistNotExistsMessage = "The artist does not exist.";

        public ConcertValidator(IQueryable<Artist> artists)
        {
            RuleFor(x => x.Artist)
                .NotNull().WithMessage(ArtistNullMessage)
                .Must(artist => artists.Any(y => y.Id == artist.Id))
                    .WithMessage(ArtistNotExistsMessage);
        }
    }
}