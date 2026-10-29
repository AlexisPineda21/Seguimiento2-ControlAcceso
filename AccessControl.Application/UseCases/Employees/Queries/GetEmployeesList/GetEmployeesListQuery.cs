using AccessControl.Application.Utilities.Mediator;
using AccessControl.Application.Utilities.Pagination;
using AccessControl.Application.Utilities.Results;
using AccessControl.Domain.Entities.Employees.ValueObjects;

namespace AccessControl.Application.UseCases.Employees.Queries.GetEmployeesList
{
    public sealed class GetEmployeesListQuery : IRequest<Result<PaginationResponse<EmployeeListItemDTO>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = PaginationRequest.DefaultPageSize;
        public Guid? AreaId { get; set; }
        public EmploymentStatus? Status { get; set; }
        public string? Search { get; set; }
    }
}
