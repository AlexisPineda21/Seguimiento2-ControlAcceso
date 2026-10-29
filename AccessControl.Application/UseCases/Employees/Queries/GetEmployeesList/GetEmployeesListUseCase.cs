using AccessControl.Application.Contracts.Repositories;
using AccessControl.Application.UseCases.Common;
using AccessControl.Application.Utilities.Mediator;
using AccessControl.Application.Utilities.Pagination;
using AccessControl.Application.Utilities.Results;
using AccessControl.Domain.Entities.Employees;

namespace AccessControl.Application.UseCases.Employees.Queries.GetEmployeesList
{
    public sealed class GetEmployeesListUseCase : BaseHandler, IRequestHandler<GetEmployeesListQuery, Result<PaginationResponse<EmployeeListItemDTO>>>
    {
        private readonly IEmployeesRepository _employeesRepository;

        public GetEmployeesListUseCase(IEmployeesRepository employeesRepository)
        {
            _employeesRepository = employeesRepository;
        }

        public Task<Result<PaginationResponse<EmployeeListItemDTO>>> Handle(GetEmployeesListQuery query)
        {
            return ExecuteAsync(async () =>
            {
                PaginationRequest pagination = new(query.PageNumber, query.PageSize);

                PaginationResponse<Employee> employees = await _employeesRepository.GetPagedListAsync(pagination,
                                                                                                      query.AreaId,
                                                                                                      query.Status,
                                                                                                      query.Search);

                return employees.Map(employee => employee.ToListItemDto());
            });
        }
    }
}
