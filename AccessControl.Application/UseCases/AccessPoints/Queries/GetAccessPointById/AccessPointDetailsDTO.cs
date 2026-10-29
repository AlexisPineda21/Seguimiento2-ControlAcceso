namespace AccessControl.Application.UseCases.AccessPoints.Queries.GetAccessPointById
{
    public sealed class AccessPointDetailsDTO
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string? Description { get; init; }
        public string Type { get; init; } = string.Empty;
        public bool IsEnabled { get; init; }
        public DateTime CreatedAt { get; init; }
    }
}
