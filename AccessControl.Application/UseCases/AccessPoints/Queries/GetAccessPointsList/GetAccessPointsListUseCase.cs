using AccessControl.Application.Contracts.Repositories;
using AccessControl.Application.UseCases.Common;
using AccessControl.Application.Utilities.Mediator;
using AccessControl.Application.Utilities.Pagination;
using AccessControl.Application.Utilities.Results;
using AccessControl.Domain.Entities.AccessPoints;

namespace AccessControl.Application.UseCases.AccessPoints.Queries.GetAccessPointsList
{
    public sealed class GetAccessPointsListUseCase : BaseHandler, IRequestHandler<GetAccessPointsListQuery, Result<PaginationResponse<AccessPointListItemDTO>>>
    {
        private readonly IAccessPointsRepository _accessPointsRepository;

        public GetAccessPointsListUseCase(IAccessPointsRepository accessPointsRepository)
        {
            _accessPointsRepository = accessPointsRepository;
        }

        public Task<Result<PaginationResponse<AccessPointListItemDTO>>> Handle(GetAccessPointsListQuery query)
        {
            return ExecuteAsync(async () =>
            {
                PaginationRequest pagination = new(query.PageNumber, query.PageSize);

                PaginationResponse<AccessPoint> accessPoints = await _accessPointsRepository.GetPagedListAsync(pagination,
                                                                                                              query.IsEnabled,
                                                                                                              query.Type);

                return accessPoints.Map(accessPoint => accessPoint.ToListItemDto());
            });
        }
    }
}
