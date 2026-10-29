using AccessControl.Api.DTOs.AccessPoints;
using AccessControl.Application.UseCases.AccessPoints.Commands.CreateAccessPoint;
using AccessControl.Application.UseCases.AccessPoints.Commands.DisableAccessPoint;
using AccessControl.Application.UseCases.AccessPoints.Commands.EnableAccessPoint;
using AccessControl.Application.UseCases.AccessPoints.Queries.GetAccessPointById;
using AccessControl.Application.UseCases.AccessPoints.Queries.GetAccessPointsList;
using AccessControl.Application.Utilities.Mediator;
using AccessControl.Application.Utilities.Pagination;
using AccessControl.Application.Utilities.Results;
using AccessControl.Domain.Entities.AccessPoints;
using Microsoft.AspNetCore.Mvc;

namespace AccessControl.Api.Controllers
{
    [Route("api/access-points")]
    public class AccessPointsController : ApiController
    {
        private readonly IMediator _mediator;

        public AccessPointsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] int pageNumber = 1,
                                                 [FromQuery] int pageSize = PaginationRequest.DefaultPageSize,
                                                 [FromQuery] bool? isEnabled = null,
                                                 [FromQuery] AccessPointType? type = null)
        {
            GetAccessPointsListQuery query = new()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                IsEnabled = isEnabled,
                Type = type
            };

            Result<PaginationResponse<AccessPointListItemDTO>> result = await _mediator.Send(query);

            return ToActionResult(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            GetAccessPointByIdQuery query = new()
            {
                Id = id
            };

            Result<AccessPointDetailsDTO> result = await _mediator.Send(query);

            return ToActionResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAccessPointDTO request)
        {
            CreateAccessPointCommand command = new()
            {
                Name = request.Name,
                Description = request.Description,
                Type = request.Type
            };

            Result<Guid> result = await _mediator.Send(command);

            return ToActionResult(result, StatusCodes.Status201Created);
        }

        [HttpPatch("{id:guid}/enable")]
        public async Task<IActionResult> Enable([FromRoute] Guid id)
        {
            EnableAccessPointCommand command = new()
            {
                Id = id
            };

            Result result = await _mediator.Send(command);

            return ToActionResult(result);
        }

        [HttpPatch("{id:guid}/disable")]
        public async Task<IActionResult> Disable([FromRoute] Guid id)
        {
            DisableAccessPointCommand command = new()
            {
                Id = id
            };

            Result result = await _mediator.Send(command);

            return ToActionResult(result);
        }
    }
}
