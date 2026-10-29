using AccessControl.Application.Utilities.Pagination;
using AccessControl.Domain.Entities.Employees;
using AccessControl.Domain.Entities.Employees.ValueObjects;

namespace AccessControl.Application.Contracts.Repositories
{
    public interface IEmployeesRepository : IRepository<Employee>
    {
        Task<Employee?> GetByIdWithAreaAsync(Guid id, CancellationToken cancellationToken = default);

        Task<PaginationResponse<Employee>> GetPagedListAsync(PaginationRequest pagination,
                                                             Guid? areaId,
                                                             EmploymentStatus? status,
                                                             string? search,
                                                             CancellationToken cancellationToken = default);

        Task<bool> ExistsByDocumentNumberAsync(DocumentNumber documentNumber, CancellationToken cancellationToken = default);

        Task<bool> ExistsByEmailAsync(Email email, Guid? excludedEmployeeId = null, CancellationToken cancellationToken = default);
    }
}
