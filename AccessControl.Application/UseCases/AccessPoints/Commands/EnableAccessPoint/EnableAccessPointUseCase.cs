using AccessControl.Application.Contracts.Persistence;
using AccessControl.Application.Contracts.Repositories;
using AccessControl.Application.Exceptions;
using AccessControl.Application.UseCases.Common;
using AccessControl.Application.Utilities.Mediator;
using AccessControl.Application.Utilities.Results;
using AccessControl.Domain.Entities.AccessPoints;

namespace AccessControl.Application.UseCases.AccessPoints.Commands.EnableAccessPoint
{
    public sealed class EnableAccessPointUseCase : BaseHandler, IRequestHandler<EnableAccessPointCommand, Result>
    {
        private readonly IAccessPointsRepository _accessPointsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EnableAccessPointUseCase(IAccessPointsRepository accessPointsRepository, IUnitOfWork unitOfWork)
        {
            _accessPointsRepository = accessPointsRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<Result> Handle(EnableAccessPointCommand command)
        {
            return ExecuteAsync(async () =>
            {
                AccessPoint? accessPoint = await _accessPointsRepository.GetByIdAsync(command.Id);

                if (accessPoint is null)
                {
                    throw new NotFoundException("El punto de acceso no existe.");
                }

                accessPoint.Enable();

                await _unitOfWork.CommitAsync();
            }, "Punto de acceso habilitado correctamente.");
        }
    }
}
