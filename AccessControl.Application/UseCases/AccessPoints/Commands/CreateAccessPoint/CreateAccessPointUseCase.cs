using AccessControl.Application.Contracts.Persistence;
using AccessControl.Application.Contracts.Repositories;
using AccessControl.Application.Exceptions;
using AccessControl.Application.UseCases.Common;
using AccessControl.Application.Utilities.Mediator;
using AccessControl.Application.Utilities.Results;
using AccessControl.Domain.Entities.AccessPoints;

namespace AccessControl.Application.UseCases.AccessPoints.Commands.CreateAccessPoint
{
    public sealed class CreateAccessPointUseCase : BaseHandler, IRequestHandler<CreateAccessPointCommand, Result<Guid>>
    {
        private readonly IAccessPointsRepository _accessPointsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateAccessPointUseCase(IAccessPointsRepository accessPointsRepository, IUnitOfWork unitOfWork)
        {
            _accessPointsRepository = accessPointsRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<Result<Guid>> Handle(CreateAccessPointCommand command)
        {
            return ExecuteAsync(async () =>
            {
                bool nameExists = await _accessPointsRepository.ExistsByNameAsync(command.Name.Trim());

                if (nameExists)
                {
                    throw new ConflictException("Ya existe un punto de acceso con ese nombre.");
                }

                AccessPoint accessPoint = new(command.Name, command.Type, command.Description);

                await _accessPointsRepository.CreateAsync(accessPoint);
                await _unitOfWork.CommitAsync();

                return accessPoint.Id;
            }, "Punto de acceso registrado correctamente.");
        }
    }
}
