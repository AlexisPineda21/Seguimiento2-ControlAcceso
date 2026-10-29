namespace AccessControl.Application.UseCases.AccessPoints.Queries.GetAccessPointsList
{
    public sealed class AccessPointListItemDTO
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Type { get; init; } = string.Empty;
        public bool IsEnabled { get; init; }
    }
}
