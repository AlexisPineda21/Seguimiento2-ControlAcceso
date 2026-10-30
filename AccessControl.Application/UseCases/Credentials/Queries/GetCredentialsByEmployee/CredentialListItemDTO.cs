namespace AccessControl.Application.UseCases.Credentials.Queries.GetCredentialsByEmployee
{
    public sealed class CredentialListItemDTO
    {
        public Guid Id { get; init; }
        public string Code { get; init; } = string.Empty;
        public string Status { get; init; } = string.Empty;
        public string? StatusReason { get; init; }
        public DateTime IssuedAt { get; init; }
    }
}
