using FluentValidation.Results;

namespace AccessControl.Application.Exceptions
{
    public class CustomValidationException : Exception
    {
        public IReadOnlyCollection<ValidationFailure> Errors { get; }

        public CustomValidationException(IReadOnlyCollection<ValidationFailure> failures)
            : base("Uno o más datos no son válidos.")
        {
            Errors = failures;
        }
    }
}
