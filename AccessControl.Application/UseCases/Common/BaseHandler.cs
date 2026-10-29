using AccessControl.Application.Exceptions;
using AccessControl.Application.Utilities.Results;
using AccessControl.Domain.Exceptions;

namespace AccessControl.Application.UseCases.Common
{
    public abstract class BaseHandler
    {
        protected async Task<Result<T>> ExecuteAsync<T>(Func<Task<T>> action, string? successMessage = null)
        {
            try
            {
                T value = await action();
                return successMessage is null ? Result<T>.Success(value) : Result<T>.Success(value, successMessage);
            }
            catch (Exception exception)
            {
                return Result<T>.Failure(MapErrors(exception));
            }
        }

        protected async Task<Result> ExecuteAsync(Func<Task> action, string? successMessage = null)
        {
            try
            {
                await action();
                return successMessage is null ? Result.Success() : Result.Success(successMessage);
            }
            catch (Exception exception)
            {
                return Result.Failure(MapErrors(exception));
            }
        }

        private static IEnumerable<Error> MapErrors(Exception exception)
        {
            return exception switch
            {
                CustomValidationException validationException => validationException.Errors.Select(failure =>
                    new Error($"Validation.{failure.PropertyName}", failure.ErrorMessage, ErrorType.Validation)),

                BussinesRuleException => [new Error("BusinessRule.Violation", exception.Message, ErrorType.Business)],

                NotFoundException => [new Error("Resource.NotFound", exception.Message, ErrorType.NotFound)],

                ConflictException => [new Error("Resource.Conflict", exception.Message, ErrorType.Conflict)],

                _ => [new Error("Server.Unexpected", "Ocurrió un error inesperado. Intente de nuevo más tarde.", ErrorType.Unexpected)]
            };
        }
    }
}
