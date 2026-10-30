namespace AccessControl.Application.UseCases.AccessLogs.Queries.GetAccessHistory
{
    public sealed class AccessLogListItemDTO
    {
        public Guid Id { get; init; }
        public DateTime OccurredAt { get; init; }
        public string CredentialCode { get; init; } = string.Empty;
        public Guid? EmployeeId { get; init; }
        public string? EmployeeName { get; init; }
        public Guid AccessPointId { get; init; }
        public string AccessPoint { get; init; } = string.Empty;
        public string MovementType { get; init; } = string.Empty;
        public string Result { get; init; } = string.Empty;
        public string? RejectionReason { get; init; }
    }
}
