using System;
using FluentValidation.TestHelper;
using MediaInventory.Core;
using MediaInventory.Tests.Common.Extensions;
using MediaInventory.Tests.Common.Fakes.Data;
using NUnit.Framework;
using Should;

namespace MediaInventory.Tests.Unit.Core
{
    [TestFixture]
    public class VenueValidatorTests
    {
        private VenueValidator _venueValidator;
        private MemoryRepository<Venue> _venues;

        [SetUp]
        public void SetUp()
        {
            _venues = new MemoryRepository<Venue>(x => x.Id);
            _venueValidator = new VenueValidator(_venues);
        }

        [Test]
        public void should_not_have_error_for_valid_venue()
        {
            _venueValidator.Validate(new Venue
            {
                Name = "The Vic",
                State = "IL",
                City = "Chicago"
            }).IsValid.ShouldBeTrue();
        }

        // name
        [TestCase(null, TestName = "should_have_error_when_name_is_null")]
        [TestCase("", TestName = "should_have_error_when_name_is_zero_length")]
        [TestCase("      ", TestName = "should_have_error_when_name_is_empty")]
        public void should_have_error_when_name_is_invalid(string name)
        {
            _venueValidator.ShouldHaveValidationErrorFor(x => x.Name, name);
        }

        [Test]
        public void should_not_have_error_when_name_is_valid()
        {
            _venueValidator.ShouldNotHaveValidationErrorFor(x => x.Name, "The Vic");
        }

        [TestCase(4, TestName = "should_have_error_when_name_is_too_short")]
        [TestCase(51, TestName = "should_have_error_when_name_is_too_long")]
        public void should_have_error_for_name_length(int length)
        {
            _venueValidator.ShouldHaveValidationErrorFor(x => x.Name, RandomString.GenerateAlphaNumeric(length));
        }

        [TestCase(5, TestName = "should_not_have_error_when_name_is_min_length")]
        [TestCase(50, TestName = "should_not_have_error_when_name_is_max_length")]
        public void should_not_have_error_for_name_length(int length)
        {
            _venueValidator.ShouldNotHaveValidationErrorFor(x => x.Name, RandomString.GenerateAlphaNumeric(length));
        }

        [Test]
        public void should_have_error_when_name_exists_with_different_id()
        {
            const string name = "The Vic";
            _venues.Add(new Venue { Name = name });

            _venueValidator.ShouldHaveValidationErrorFor(x => x.Name, name);
        }

        [Test]
        public void should_not_have_error_when_name_exists_with_same_id()
        {
            var artist = _venues.Add(new Venue { Id = Guid.NewGuid(), Name = "The Vic" });

            _venueValidator.ShouldNotHaveValidationErrorFor(x => x.Name, artist);
        }

        // state
        [TestCase(null, TestName = "should_have_error_when_state_is_null")]
        [TestCase("", TestName = "should_have_error_when_state_is_zero_length")]
        [TestCase("      ", TestName = "should_have_error_when_state_is_empty")]
        public void should_have_error_when_state_is_invalid(string state)
        {
            _venueValidator.ShouldHaveValidationErrorFor(x => x.State, state);
        }

        [Test]
        public void should_not_have_error_when_state_is_valid()
        {
            _venueValidator.ShouldNotHaveValidationErrorFor(x => x.State, "IL");
        }

        [TestCase(1, TestName = "should_have_error_when_state_is_too_short")]
        [TestCase(3, TestName = "should_have_error_when_state_is_too_long")]
        public void should_have_error_for_state_length(int length)
        {
            _venueValidator.ShouldHaveValidationErrorFor(x => x.State, RandomString.GenerateAlphaNumeric(length));
        }

        [Test]
        public void should_not_have_error_when_state_is_valid_length()
        {
            _venueValidator.ShouldNotHaveValidationErrorFor(x => x.State, RandomString.GenerateAlphaNumeric(2));
        }

        // city
        [TestCase(null, TestName = "should_have_error_when_city_is_null")]
        [TestCase("", TestName = "should_have_error_when_city_is_zero_length")]
        [TestCase("      ", TestName = "should_have_error_when_city_is_empty")]
        public void should_have_error_when_city_is_invalid(string city)
        {
            _venueValidator.ShouldHaveValidationErrorFor(x => x.City, city);
        }

        [Test]
        public void should_not_have_error_when_city_is_valid()
        {
            _venueValidator.ShouldNotHaveValidationErrorFor(x => x.City, "Chicago");
        }

        [TestCase(1, TestName = "should_have_error_when_city_is_too_short")]
        [TestCase(46, TestName = "should_have_error_when_city_is_too_long")]
        public void should_have_error_for_city_length(int length)
        {
            _venueValidator.ShouldHaveValidationErrorFor(x => x.City, RandomString.GenerateAlphaNumeric(length));
        }

        [TestCase(2, TestName = "should_not_have_error_when_city_is_min_length")]
        [TestCase(45, TestName = "should_not_have_error_when_city_is_max_length")]
        public void should_not_have_error_for_city_length(int length)
        {
            _venueValidator.ShouldNotHaveValidationErrorFor(x => x.City, RandomString.GenerateAlphaNumeric(length));
        }
    }
}