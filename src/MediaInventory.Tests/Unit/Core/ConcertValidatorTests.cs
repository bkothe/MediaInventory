using FluentValidation.TestHelper;
using MediaInventory.Core;
using MediaInventory.Tests.Common.Fakes.Data;
using NUnit.Framework;
using Should;

namespace MediaInventory.Tests.Unit.Core
{
    [TestFixture]
    public class ConcertValidatorTests
    {
        private ConcertValidator _concertValidator;
        private MemoryRepository<Artist> _artists;

        [SetUp]
        public void SetUp()
        {
            _artists = new MemoryRepository<Artist>(x => x.Id);
            _concertValidator = new ConcertValidator(_artists);
        }

        [Test]
        public void should_not_have_error_for_valid_concert()
        {
            _concertValidator.Validate(new Concert
            {
                Artist = _artists.Add(new Artist())
            }).IsValid.ShouldBeTrue();
        }

        [Test]
        public void should_have_error_when_artist_is_null()
        {
            _concertValidator.ShouldHaveValidationErrorFor(x => x.Artist, new Concert { Artist = null });
        }

        [Test]
        public void should_have_error_when_artist_does_not_exist()
        {
            _concertValidator.ShouldHaveValidationErrorFor(x => x.Artist, new Artist());
        }

        [Test]
        public void should_not_have_error_when_artist_exists()
        {
            _concertValidator.ShouldNotHaveValidationErrorFor(x => x.Artist, _artists.Add(new Artist()));
        }
    }
}