using AccessControl.Application.Contracts.Persistence;
using AccessControl.Application.Contracts.Repositories;
using AccessControl.Application.Exceptions;
using AccessControl.Application.UseCases.Common;
using AccessControl.Application.Utilities.Mediator;
using AccessControl.Application.Utilities.Results;
using AccessControl.Domain.Entities.AccessPoints;

namespace AccessControl.Application.UseCases.AccessPoints.Commands.DisableAccessPoint
{
    public sealed class DisableAccessPointUseCase : BaseHandler, IRequestHandler<DisableAccessPointCommand, Result>
    {
        private readonly IAccessPointsRepository _accessPointsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DisableAccessPointUseCase(IAccessPointsRepository accessPointsRepository, IUnitOfWork unitOfWork)
        {
            _accessPointsRepository = accessPointsRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<Result> Handle(DisableAccessPointCommand command)
        {
            return ExecuteAsync(async () =>
            {
                AccessPoint? accessPoint = await _accessPointsRepository.GetByIdAsync(command.Id);

                if (accessPoint is null)
                {
                    throw new NotFoundException("El punto de acceso no existe.");
                }

                accessPoint.Disable();

                await _unitOfWork.CommitAsync();
            }, "Punto de acceso deshabilitado correctamente.");
        }
    }
}
