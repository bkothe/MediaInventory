using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;

namespace MediaInventory.Infrastructure.Common.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public ValidationException(string message) : base(message) { Messages = new List<string> { message }; }
        public ValidationException(string message, Exception innerException) : base(message, innerException) { Messages = new List<string> { message }; }
        public ValidationException(IEnumerable<string> messages) : base(GetFullMessage(messages)) { Messages = messages; }
        public ValidationException(IEnumerable<string> messages, Exception innerException) : base(GetFullMessage(messages), innerException) { Messages = messages; }
        public ValidationException(ValidationResult result) : this(result.Errors.Select(x => x.ErrorMessage)) { }
        public ValidationException(ValidationResult result, Exception innerException) : this(result.Errors.Select(x => x.ErrorMessage), innerException) { }

        public IEnumerable<string> Messages { get; private set; }

        private static string GetFullMessage(IEnumerable<string> messages)
        {
            return messages.Select(x => x.Trim(' ', '.')).Aggregate((a, i) => a + ". " + i) + ".";
        }
    }

    public static class ValidationExtensions
    {
        public static void ValidateAndThrow<T>(this IValidator<T> validator, T instance)
        {
            var result = validator.Validate(instance);
            if (!result.IsValid) throw new ValidationException(result);
        }
    }
}