using AccessControl.Api.DTOs.AccessLogs;
using AccessControl.Application.UseCases.AccessLogs.Commands.RegisterAccess;
using AccessControl.Application.UseCases.AccessLogs.Queries.GetAccessHistory;
using AccessControl.Application.Utilities.Mediator;
using AccessControl.Application.Utilities.Pagination;
using AccessControl.Application.Utilities.Results;
using AccessControl.Domain.Entities.AccessLogs;
using Microsoft.AspNetCore.Mvc;

namespace AccessControl.Api.Controllers
{
    [Route("api/access")]
    public class AccessController : ApiController
    {
        private readonly IMediator _mediator;

        public AccessController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterAccessDTO request)
        {
            RegisterAccessCommand command = new()
            {
                CredentialCode = request.CredentialCode,
                AccessPointId = request.AccessPointId,
                MovementType = request.MovementType
            };

            Result<RegisterAccessResultDTO> result = await _mediator.Send(command);

            return ToActionResult(result, StatusCodes.Status201Created);
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetHistory([FromQuery] int pageNumber = 1,
                                                    [FromQuery] int pageSize = PaginationRequest.DefaultPageSize,
                                                    [FromQuery] Guid? employeeId = null,
                                                    [FromQuery] Guid? accessPointId = null,
                                                    [FromQuery] DateOnly? fromDate = null,
                                                    [FromQuery] DateOnly? toDate = null,
                                                    [FromQuery] AccessResult? result = null)
        {
            GetAccessHistoryQuery query = new()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                EmployeeId = employeeId,
                AccessPointId = accessPointId,
                FromDate = fromDate,
                ToDate = toDate,
                Result = result
            };

            Result<PaginationResponse<AccessLogListItemDTO>> response = await _mediator.Send(query);

            return ToActionResult(response);
        }
    }
}
