using AccessControl.Application.Contracts.Repositories;
using AccessControl.Application.UseCases.Common;
using AccessControl.Application.Utilities.Mediator;
using AccessControl.Application.Utilities.Results;
using AccessControl.Domain.Entities.Areas;

namespace AccessControl.Application.UseCases.Areas.Queries.GetAreasList
{
    public sealed class GetAreasListUseCase : BaseHandler, IRequestHandler<GetAreasListQuery, Result<IReadOnlyList<AreaDTO>>>
    {
        private readonly IAreasRepository _areasRepository;

        public GetAreasListUseCase(IAreasRepository areasRepository)
        {
            _areasRepository = areasRepository;
        }

        public Task<Result<IReadOnlyList<AreaDTO>>> Handle(GetAreasListQuery query)
        {
            return ExecuteAsync<IReadOnlyList<AreaDTO>>(async () =>
            {
                IReadOnlyList<Area> areas = await _areasRepository.GetListAsync();

                return areas.OrderBy(area => area.Name)
                            .Select(area => new AreaDTO
                            {
                                Id = area.Id,
                                Name = area.Name,
                                Description = area.Description
                            })
                            .ToList();
            });
        }
    }
}
