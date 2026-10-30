using AccessControl.Api.DTOs.AccessLogs;
using AccessControl.Application.UseCases.AccessLogs.Commands.RegisterAccess;
using AccessControl.Application.Utilities.Mediator;
using AccessControl.Application.Utilities.Results;
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
    }
}
