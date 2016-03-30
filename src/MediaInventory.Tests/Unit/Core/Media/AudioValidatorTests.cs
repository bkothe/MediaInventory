using FluentValidation.TestHelper;
using MediaInventory.Core.Media;
using MediaInventory.Tests.Common.Extensions;
using MediaInventory.Tests.Common.Fakes.Data;
using NUnit.Framework;
using Should;

namespace MediaInventory.Tests.Unit.Core.Media
{
    [TestFixture]
    public class AudioValidatorTests
    {
        private MemoryRepository<MediaInventory.Core.Artist.Artist> _artists;
        private AudioValidator _audioValidator;

        [SetUp]
        public void SetUp()
        {
            _artists = new MemoryRepository<MediaInventory.Core.Artist.Artist>(x => x.Id);
            _audioValidator = new AudioValidator(_artists);
        }

        [Test]
        public void should_not_have_error_for_valid_audio()
        {
            _audioValidator.Validate(new Audio
            {
                Artist = _artists.Add(new MediaInventory.Core.Artist.Artist()),
                Title = "2112",
                MediaFormat = MediaFormat.Vinyl,
                MediaCount = 1
            }).IsValid.ShouldBeTrue();
        }

        [Test]
        public void should_have_error_when_artist_is_null()
        {
            _audioValidator.ShouldHaveValidationErrorFor(x => x.Artist, new Audio { Artist = null });
        }

        [Test]
        public void should_have_error_when_artist_does_not_exist()
        {
            _audioValidator.ShouldHaveValidationErrorFor(x => x.Artist, new MediaInventory.Core.Artist.Artist());
        }

        [Test]
        public void should_not_have_error_when_artist_exists()
        {
            _audioValidator.ShouldNotHaveValidationErrorFor(x => x.Artist, _artists.Add(new MediaInventory.Core.Artist.Artist()));
        }

        [TestCase(null, TestName = "should_have_error_when_title_is_null")]
        [TestCase("", TestName = "should_have_error_when_title_is_zero_length")]
        [TestCase("    ", TestName = "should_have_error when_title_is_empty")]
        public void should_have_error_when_title_is_invalid(string title)
        {
            _audioValidator.ShouldHaveValidationErrorFor(x => x.Title, title);
        }

        [Test]
        public void should_not_have_error_when_title_is_valid()
        {
            _audioValidator.ShouldNotHaveValidationErrorFor(x => x.Title, "Rush");
        }

        [TestCase(0, TestName = "should_have_error_when_title_is_too_short")]
        [TestCase(86, TestName = "should_have_error_when_title_is_too_long")]
        public void should_have_error_for_name_length(int length)
        {
            _audioValidator.ShouldHaveValidationErrorFor(x => x.Title, RandomString.GenerateAlphaNumeric(length));
        }

        [TestCase(1, TestName = "should_not_have_error_when_title_is_min_length")]
        [TestCase(85, TestName = "should_not_have_error_when_title_is_max_length")]
        public void should_not_have_error_for_name_length(int length)
        {
            _audioValidator.ShouldNotHaveValidationErrorFor(x => x.Title, RandomString.GenerateAlphaNumeric(length));
        }

        [Test]
        public void should_not_have_error_when_media_format_is_not_null()
        {
            _audioValidator.ShouldNotHaveValidationErrorFor(x => x.MediaFormat, MediaFormat.Vinyl);
        }

        [TestCase(null, TestName = "should_have_error_when_media_count_is_null")]
        [TestCase(0, TestName = "should_have_error_when_media_count_is_zero")]
        public void should_have_error_when_media_count_is_invalid(int mediaCount)
        {
            _audioValidator.ShouldHaveValidationErrorFor(x => x.MediaCount, mediaCount);
        }

        [Test]
        public void should_not_have_error_when_media_count_is_greater_than_zero()
        {
            _audioValidator.ShouldNotHaveValidationErrorFor(x => x.MediaFormat, new Audio { MediaCount = 1 });
        }
    }
}