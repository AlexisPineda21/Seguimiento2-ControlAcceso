namespace AccessControl.Application.UseCases.Credentials.Queries.GetCredentialByCode
{
    public sealed class CredentialDetailsDTO
    {
        public Guid Id { get; init; }
        public string Code { get; init; } = string.Empty;
        public string Status { get; init; } = string.Empty;
        public bool CanBeUsed { get; init; }
        public string? StatusReason { get; init; }
        public Guid EmployeeId { get; init; }
        public string EmployeeName { get; init; } = string.Empty;
        public string EmployeeDocumentNumber { get; init; } = string.Empty;
        public bool EmployeeIsActive { get; init; }
        public DateTime IssuedAt { get; init; }
        public DateTime? BlockedAt { get; init; }
        public DateTime? RevokedAt { get; init; }
    }
}
