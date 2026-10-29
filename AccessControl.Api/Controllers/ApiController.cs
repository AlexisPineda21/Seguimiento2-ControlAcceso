using AccessControl.Api.Responses;
using AccessControl.Application.Utilities.Results;
using Microsoft.AspNetCore.Mvc;

namespace AccessControl.Api.Controllers
{
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
        protected IActionResult ToActionResult<T>(Result<T> result, int successStatusCode = StatusCodes.Status200OK)
        {
            if (result.IsFailure)
            {
                return ToErrorResult<T>(result);
            }

            return StatusCode(successStatusCode, ApiResponse<T>.Ok(result.Value, result.Message));
        }

        protected IActionResult ToActionResult(Result result)
        {
            if (result.IsFailure)
            {
                return ToErrorResult<object>(result);
            }

            return StatusCode(StatusCodes.Status200OK, ApiResponse<object>.Ok(null, result.Message));
        }

        private IActionResult ToErrorResult<T>(Result result)
        {
            ErrorType errorType = result.Errors.FirstOrDefault()?.Type ?? ErrorType.Unexpected;
            ApiResponse<T> body = ApiResponse<T>.Fail(ApiErrorMapper.FromMany(result.Errors), result.Message);

            int statusCode = errorType switch
            {
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.Business => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };

            return StatusCode(statusCode, body);
        }
    }
}
