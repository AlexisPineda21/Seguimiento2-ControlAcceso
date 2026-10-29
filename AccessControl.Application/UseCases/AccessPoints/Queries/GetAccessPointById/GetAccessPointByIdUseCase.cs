using AccessControl.Application.Contracts.Repositories;
using AccessControl.Application.Exceptions;
using AccessControl.Application.UseCases.Common;
using AccessControl.Application.Utilities.Mediator;
using AccessControl.Application.Utilities.Results;
using AccessControl.Domain.Entities.AccessPoints;

namespace AccessControl.Application.UseCases.AccessPoints.Queries.GetAccessPointById
{
    public sealed class GetAccessPointByIdUseCase : BaseHandler, IRequestHandler<GetAccessPointByIdQuery, Result<AccessPointDetailsDTO>>
    {
        private readonly IAccessPointsRepository _accessPointsRepository;

        public GetAccessPointByIdUseCase(IAccessPointsRepository accessPointsRepository)
        {
            _accessPointsRepository = accessPointsRepository;
        }

        public Task<Result<AccessPointDetailsDTO>> Handle(GetAccessPointByIdQuery query)
        {
            return ExecuteAsync(async () =>
            {
                AccessPoint? accessPoint = await _accessPointsRepository.GetByIdAsync(query.Id);

                if (accessPoint is null)
                {
                    throw new NotFoundException("El punto de acceso no existe.");
                }

                return accessPoint.ToDetailsDto();
            });
        }
    }
}
