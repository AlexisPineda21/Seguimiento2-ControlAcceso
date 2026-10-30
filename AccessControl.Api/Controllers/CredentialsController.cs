using AccessControl.Api.DTOs.Credentials;
using AccessControl.Application.UseCases.Credentials.Commands.AssignCredential;
using AccessControl.Application.UseCases.Credentials.Commands.BlockCredential;
using AccessControl.Application.UseCases.Credentials.Commands.RevokeCredential;
using AccessControl.Application.UseCases.Credentials.Commands.UnblockCredential;
using AccessControl.Application.UseCases.Credentials.Queries.GetCredentialByCode;
using AccessControl.Application.UseCases.Credentials.Queries.GetCredentialsByEmployee;
using AccessControl.Application.Utilities.Mediator;
using AccessControl.Application.Utilities.Results;
using Microsoft.AspNetCore.Mvc;

namespace AccessControl.Api.Controllers
{
    [Route("api/credentials")]
    public class CredentialsController : ApiController
    {
        private readonly IMediator _mediator;

        public CredentialsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("code/{code}")]
        public async Task<IActionResult> GetByCode([FromRoute] string code)
        {
            GetCredentialByCodeQuery query = new()
            {
                Code = code
            };

            Result<CredentialDetailsDTO> result = await _mediator.Send(query);

            return ToActionResult(result);
        }

        [HttpGet("employee/{employeeId:guid}")]
        public async Task<IActionResult> GetByEmployee([FromRoute] Guid employeeId)
        {
            GetCredentialsByEmployeeQuery query = new()
            {
                EmployeeId = employeeId
            };

            Result<IReadOnlyList<CredentialListItemDTO>> result = await _mediator.Send(query);

            return ToActionResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Assign([FromBody] AssignCredentialDTO request)
        {
            AssignCredentialCommand command = new()
            {
                EmployeeId = request.EmployeeId,
                Code = request.Code
            };

            Result<Guid> result = await _mediator.Send(command);

            return ToActionResult(result, StatusCodes.Status201Created);
        }

        [HttpPatch("{id:guid}/block")]
        public async Task<IActionResult> Block([FromRoute] Guid id, [FromBody] CredentialReasonDTO request)
        {
            BlockCredentialCommand command = new()
            {
                Id = id,
                Reason = request.Reason
            };

            Result result = await _mediator.Send(command);

            return ToActionResult(result);
        }

        [HttpPatch("{id:guid}/unblock")]
        public async Task<IActionResult> Unblock([FromRoute] Guid id)
        {
            UnblockCredentialCommand command = new()
            {
                Id = id
            };

            Result result = await _mediator.Send(command);

            return ToActionResult(result);
        }

        [HttpPatch("{id:guid}/revoke")]
        public async Task<IActionResult> Revoke([FromRoute] Guid id, [FromBody] CredentialReasonDTO request)
        {
            RevokeCredentialCommand command = new()
            {
                Id = id,
                Reason = request.Reason
            };

            Result result = await _mediator.Send(command);

            return ToActionResult(result);
        }
    }
}
