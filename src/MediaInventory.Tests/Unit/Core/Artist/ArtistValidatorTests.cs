using System;
using FluentValidation.TestHelper;
using MediaInventory.Core.Artist;
using MediaInventory.Tests.Common.Extensions;
using MediaInventory.Tests.Common.Fakes.Data;
using NUnit.Framework;
using Should;

namespace MediaInventory.Tests.Unit.Core.Artist
{
    [TestFixture]
    public class ArtistValidatorTests
    {
        private ArtistValidator _artistValidator;
        private MemoryRepository<MediaInventory.Core.Artist.Artist> _artists;

        [SetUp]
        public void SetUp()
        {
            _artists =  new MemoryRepository<MediaInventory.Core.Artist.Artist>(x => x.Id);
            _artistValidator = new ArtistValidator(_artists);
        }

        [Test]
        public void should_not_have_error_for_valid_artist()
        {
            _artistValidator.Validate(new MediaInventory.Core.Artist.Artist
            {
                Name = "Rush"
            }).IsValid.ShouldBeTrue();
        }

        [TestCase(null, TestName = "should_have_error_when_name_is_null")]
        [TestCase("", TestName = "should_have_error_when_name_is_zero_length")]
        [TestCase("    ", TestName = "should_have_error when_name_is_empty")]
        public void should_have_error_when_name_is_invalid(string name)
        {
            _artistValidator.ShouldHaveValidationErrorFor(x => x.Name, name);
        }

        [Test]
        public void should_not_have_error_when_name_is_valid()
        {
            _artistValidator.ShouldNotHaveValidationErrorFor(x => x.Name, "Rush");
        }

        [TestCase(1, TestName = "should_have_error_when_name_is_too_short")]
        [TestCase(66, TestName = "should_have_error_when_name_is_too_long")]
        public void should_have_error_for_name_length(int length)
        {
            _artistValidator.ShouldHaveValidationErrorFor(x => x.Name, RandomString.GenerateAlphaNumeric(length));
        }

        [TestCase(2, TestName = "should_not_have_error_when_name_is_min_length")]
        [TestCase(65, TestName = "should_not_have_error_when_name_is_max_length")]
        public void should_not_have_error_for_name_length(int length)
        {
            _artistValidator.ShouldNotHaveValidationErrorFor(x => x.Name, RandomString.GenerateAlphaNumeric(length));
        }

        [Test]
        public void should_have_error_when_name_exists_with_different_id()
        {
            const string name = "Rush";
            _artists.Add(new MediaInventory.Core.Artist.Artist { Name = name });

            _artistValidator.ShouldHaveValidationErrorFor(x => x.Name, name);
        }

        [Test]
        public void should_not_have_error_when_name_exists_with_same_id()
        {
            var artist = _artists.Add(new MediaInventory.Core.Artist.Artist { Id = Guid.NewGuid(), Name = "Rush" });

            _artistValidator.ShouldNotHaveValidationErrorFor(x => x.Name, artist);
        }
    }
}