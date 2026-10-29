using AccessControl.Application.Utilities.Results;

namespace AccessControl.Api.Responses
{
    internal static class ApiErrorMapper
    {
        public static IReadOnlyList<ApiError> FromMany(IReadOnlyCollection<Error> errors)
        {
            return errors.Select(error => new ApiError(error.Code, error.Description, MapType(error.Type)))
                         .ToList();
        }

        private static string MapType(ErrorType type)
        {
            return type switch
            {
                ErrorType.Validation => "validation",
                ErrorType.Business => "business",
                ErrorType.NotFound => "notfound",
                ErrorType.Conflict => "conflict",
                ErrorType.None => "none",
                _ => "unexpected"
            };
        }
    }
}
