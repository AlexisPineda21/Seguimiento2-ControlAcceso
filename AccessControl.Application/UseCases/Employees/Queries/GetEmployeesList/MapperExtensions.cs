using AccessControl.Domain.Entities.Employees;

namespace AccessControl.Application.UseCases.Employees.Queries.GetEmployeesList
{
    public static class MapperExtensions
    {
        public static EmployeeListItemDTO ToListItemDto(this Employee employee)
        {
            return new EmployeeListItemDTO
            {
                Id = employee.Id,
                DocumentNumber = employee.DocumentNumber.Value,
                FullName = employee.FullName,
                Email = employee.Email.Value,
                Position = employee.Position,
                Area = employee.Area?.Name ?? string.Empty,
                Status = employee.Status.ToString()
            };
        }
    }
}
