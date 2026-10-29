using AccessControl.Domain.Entities.AccessPoints;

namespace AccessControl.Application.UseCases.AccessPoints.Queries.GetAccessPointsList
{
    public static class MapperExtensions
    {
        public static AccessPointListItemDTO ToListItemDto(this AccessPoint accessPoint)
        {
            return new AccessPointListItemDTO
            {
                Id = accessPoint.Id,
                Name = accessPoint.Name,
                Type = accessPoint.Type.ToString(),
                IsEnabled = accessPoint.IsEnabled
            };
        }
    }
}
