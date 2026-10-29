using AccessControl.Domain.Entities.AccessPoints;

namespace AccessControl.Application.UseCases.AccessPoints.Queries.GetAccessPointById
{
    public static class MapperExtensions
    {
        public static AccessPointDetailsDTO ToDetailsDto(this AccessPoint accessPoint)
        {
            return new AccessPointDetailsDTO
            {
                Id = accessPoint.Id,
                Name = accessPoint.Name,
                Description = accessPoint.Description,
                Type = accessPoint.Type.ToString(),
                IsEnabled = accessPoint.IsEnabled,
                CreatedAt = accessPoint.CreatedAt
            };
        }
    }
}
