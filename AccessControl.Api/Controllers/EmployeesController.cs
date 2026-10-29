using AccessControl.Api.DTOs.Employees;
using AccessControl.Application.UseCases.Employees.Commands.CreateEmployee;
using AccessControl.Application.UseCases.Employees.Commands.DeactivateEmployee;
using AccessControl.Application.UseCases.Employees.Commands.UpdateEmployee;
using AccessControl.Application.UseCases.Employees.Queries.GetEmployeeById;
using AccessControl.Application.UseCases.Employees.Queries.GetEmployeesList;
using AccessControl.Application.Utilities.Mediator;
using AccessControl.Application.Utilities.Pagination;
using AccessControl.Application.Utilities.Results;
using AccessControl.Domain.Entities.Employees.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace AccessControl.Api.Controllers
{
    [Route("api/employees")]
    public class EmployeesController : ApiController
    {
        private readonly IMediator _mediator;

        public EmployeesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] int pageNumber = 1,
                                                 [FromQuery] int pageSize = PaginationRequest.DefaultPageSize,
                                                 [FromQuery] Guid? areaId = null,
                                                 [FromQuery] EmploymentStatus? status = null,
                                                 [FromQuery] string? search = null)
        {
            GetEmployeesListQuery query = new()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                AreaId = areaId,
                Status = status,
                Search = search
            };

            Result<PaginationResponse<EmployeeListItemDTO>> result = await _mediator.Send(query);

            return ToActionResult(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            GetEmployeeByIdQuery query = new()
            {
                Id = id
            };

            Result<EmployeeDetailsDTO> result = await _mediator.Send(query);

            return ToActionResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeDTO request)
        {
            CreateEmployeeCommand command = new()
            {
                DocumentNumber = request.DocumentNumber,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Position = request.Position,
                AreaId = request.AreaId
            };

            Result<Guid> result = await _mediator.Send(command);

            return ToActionResult(result, StatusCodes.Status201Created);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateEmployeeDTO request)
        {
            UpdateEmployeeCommand command = new()
            {
                Id = id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Position = request.Position,
                AreaId = request.AreaId
            };

            Result result = await _mediator.Send(command);

            return ToActionResult(result);
        }

        [HttpPatch("{id:guid}/deactivate")]
        public async Task<IActionResult> Deactivate([FromRoute] Guid id)
        {
            DeactivateEmployeeCommand command = new()
            {
                Id = id
            };

            Result result = await _mediator.Send(command);

            return ToActionResult(result);
        }
    }
}
