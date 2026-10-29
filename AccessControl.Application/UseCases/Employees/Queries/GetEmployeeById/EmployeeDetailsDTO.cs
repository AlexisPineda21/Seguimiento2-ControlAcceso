namespace AccessControl.Application.UseCases.Employees.Queries.GetEmployeeById
{
    public sealed class EmployeeDetailsDTO
    {
        public Guid Id { get; init; }
        public string DocumentNumber { get; init; } = string.Empty;
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string Position { get; init; } = string.Empty;
        public Guid AreaId { get; init; }
        public string Area { get; init; } = string.Empty;
        public string Status { get; init; } = string.Empty;
        public DateTime CreatedAt { get; init; }
        public DateTime? DeactivatedAt { get; init; }
    }
}
