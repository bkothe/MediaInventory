using System.Linq;
using FluentValidation;

namespace MediaInventory.Core.Performance
{
    public class ConcertValidator : AbstractValidator<Concert>
    {
        public const string ArtistNullMessage = "An artist is required.";
        public const string ArtistNotExistsMessage = "The artist does not exist.";
        public const string VenueNullMessage = "A venue is required.";
        public const string VenueNotExistsMessage = "The venue does not exist.";

        public ConcertValidator(IQueryable<Artist.Artist> artists, IQueryable<Venue.Venue> venues)
        {
            RuleFor(x => x.Artist)
                .NotNull().WithMessage(ArtistNullMessage)
                .Must(artist => artists.Any(y => y.Id == artist.Id))
                    .WithMessage(ArtistNotExistsMessage);

            RuleFor(x => x.Venue)
                .NotNull().WithMessage(VenueNullMessage)
                .Must(venue => venues.Any(y => y.Id == venue.Id))
                    .WithMessage(VenueNotExistsMessage);
        }
    }
}