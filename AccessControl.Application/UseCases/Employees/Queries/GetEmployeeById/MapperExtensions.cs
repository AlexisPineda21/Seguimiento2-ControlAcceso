using AccessControl.Domain.Entities.Employees;

namespace AccessControl.Application.UseCases.Employees.Queries.GetEmployeeById
{
    public static class MapperExtensions
    {
        public static EmployeeDetailsDTO ToDetailsDto(this Employee employee)
        {
            return new EmployeeDetailsDTO
            {
                Id = employee.Id,
                DocumentNumber = employee.DocumentNumber.Value,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email.Value,
                Position = employee.Position,
                AreaId = employee.AreaId,
                Area = employee.Area?.Name ?? string.Empty,
                Status = employee.Status.ToString(),
                CreatedAt = employee.CreatedAt,
                DeactivatedAt = employee.DeactivatedAt
            };
        }
    }
}
