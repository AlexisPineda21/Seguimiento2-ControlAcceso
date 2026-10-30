namespace AccessControl.Application.UseCases.AccessLogs.Commands.RegisterAccess
{
    public sealed class RegisterAccessResultDTO
    {
        public Guid AccessLogId { get; init; }
        public string Result { get; init; } = string.Empty;
        public string? RejectionReason { get; init; }
        public string CredentialCode { get; init; } = string.Empty;
        public string? EmployeeName { get; init; }
        public string AccessPoint { get; init; } = string.Empty;
        public string MovementType { get; init; } = string.Empty;
        public DateTime OccurredAt { get; init; }
    }
}
