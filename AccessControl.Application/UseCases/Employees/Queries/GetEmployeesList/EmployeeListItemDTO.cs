namespace AccessControl.Application.UseCases.Employees.Queries.GetEmployeesList
{
    public sealed class EmployeeListItemDTO
    {
        public Guid Id { get; init; }
        public string DocumentNumber { get; init; } = string.Empty;
        public string FullName { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string Position { get; init; } = string.Empty;
        public string Area { get; init; } = string.Empty;
        public string Status { get; init; } = string.Empty;
    }
}
