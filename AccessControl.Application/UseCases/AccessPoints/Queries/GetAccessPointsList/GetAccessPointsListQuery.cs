using AccessControl.Application.Utilities.Mediator;
using AccessControl.Application.Utilities.Pagination;
using AccessControl.Application.Utilities.Results;
using AccessControl.Domain.Entities.AccessPoints;

namespace AccessControl.Application.UseCases.AccessPoints.Queries.GetAccessPointsList
{
    public sealed class GetAccessPointsListQuery : IRequest<Result<PaginationResponse<AccessPointListItemDTO>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = PaginationRequest.DefaultPageSize;
        public bool? IsEnabled { get; set; }
        public AccessPointType? Type { get; set; }
    }
}
