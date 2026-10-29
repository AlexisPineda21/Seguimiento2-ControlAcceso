using AccessControl.Application.Contracts.Repositories;
using AccessControl.Application.Utilities.Pagination;
using AccessControl.Domain.Entities.Employees;
using AccessControl.Domain.Entities.Employees.ValueObjects;
using AccessControl.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace AccessControl.Persistence.Repositories
{
    public class EmployeesRepository : Repository<Employee>, IEmployeesRepository
    {
        private readonly DataContext _context;

        public EmployeesRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Employee?> GetByIdWithAreaAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Employees.Include(employee => employee.Area)
                                           .FirstOrDefaultAsync(employee => employee.Id == id, cancellationToken);
        }

        public async Task<PaginationResponse<Employee>> GetPagedListAsync(PaginationRequest pagination,
                                                                          Guid? areaId,
                                                                          EmploymentStatus? status,
                                                                          string? search,
                                                                          CancellationToken cancellationToken = default)
        {
            IQueryable<Employee> query = _context.Employees.AsNoTracking()
                                                           .Include(employee => employee.Area);

            if (areaId.HasValue)
            {
                query = query.Where(employee => employee.AreaId == areaId.Value);
            }

            if (status.HasValue)
            {
                query = query.Where(employee => employee.Status == status.Value);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                string term = search.Trim();

                query = query.Where(employee => employee.FirstName.Contains(term) ||
                                                employee.LastName.Contains(term) ||
                                                employee.DocumentNumber.Value.Contains(term));
            }

            query = query.OrderBy(employee => employee.LastName)
                         .ThenBy(employee => employee.FirstName);

            return await query.ToPagedListAsync(pagination, cancellationToken);
        }

        public async Task<bool> ExistsByDocumentNumberAsync(DocumentNumber documentNumber, CancellationToken cancellationToken = default)
        {
            return await _context.Employees.AnyAsync(employee => employee.DocumentNumber.Value == documentNumber.Value, cancellationToken);
        }

        public async Task<bool> ExistsByEmailAsync(Email email, Guid? excludedEmployeeId = null, CancellationToken cancellationToken = default)
        {
            return await _context.Employees.AnyAsync(employee => employee.Email.Value == email.Value &&
                                                                 employee.Id != excludedEmployeeId,
                                                     cancellationToken);
        }
    }
}
