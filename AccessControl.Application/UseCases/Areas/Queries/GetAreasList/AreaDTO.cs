namespace AccessControl.Application.UseCases.Areas.Queries.GetAreasList
{
    public sealed class AreaDTO
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string? Description { get; init; }
    }
}
