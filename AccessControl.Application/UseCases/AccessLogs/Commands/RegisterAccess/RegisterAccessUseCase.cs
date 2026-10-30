using AccessControl.Application.Contracts.Persistence;
using AccessControl.Application.Contracts.Repositories;
using AccessControl.Application.Exceptions;
using AccessControl.Application.UseCases.Common;
using AccessControl.Application.Utilities.Mediator;
using AccessControl.Application.Utilities.Results;
using AccessControl.Domain.Entities.AccessLogs;
using AccessControl.Domain.Entities.AccessPoints;
using AccessControl.Domain.Entities.Credentials;

namespace AccessControl.Application.UseCases.AccessLogs.Commands.RegisterAccess
{
    public sealed class RegisterAccessUseCase : BaseHandler, IRequestHandler<RegisterAccessCommand, Result<RegisterAccessResultDTO>>
    {
        private readonly IAccessLogsRepository _accessLogsRepository;
        private readonly ICredentialsRepository _credentialsRepository;
        private readonly IAccessPointsRepository _accessPointsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterAccessUseCase(IAccessLogsRepository accessLogsRepository,
                                     ICredentialsRepository credentialsRepository,
                                     IAccessPointsRepository accessPointsRepository,
                                     IUnitOfWork unitOfWork)
        {
            _accessLogsRepository = accessLogsRepository;
            _credentialsRepository = credentialsRepository;
            _accessPointsRepository = accessPointsRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<Result<RegisterAccessResultDTO>> Handle(RegisterAccessCommand command)
        {
            return ExecuteAsync(async () =>
            {
                AccessPoint? accessPoint = await _accessPointsRepository.GetByIdAsync(command.AccessPointId);

                if (accessPoint is null)
                {
                    throw new NotFoundException("El punto de acceso no existe.");
                }

                string code = Credential.NormalizeCode(command.CredentialCode);
                Credential? credential = await _credentialsRepository.GetByCodeAsync(code);

                string? rejectionReason = await GetRejectionReasonAsync(credential, accessPoint, command.MovementType);

                AccessLog accessLog = rejectionReason is null
                    ? AccessLog.Authorize(credential!, accessPoint.Id, command.MovementType)
                    : AccessLog.Reject(code, accessPoint.Id, command.MovementType, rejectionReason, credential);

                await _accessLogsRepository.CreateAsync(accessLog);
                await _unitOfWork.CommitAsync();

                return new RegisterAccessResultDTO
                {
                    AccessLogId = accessLog.Id,
                    Result = accessLog.Result.ToString(),
                    RejectionReason = accessLog.RejectionReason,
                    CredentialCode = accessLog.CredentialCode,
                    EmployeeName = credential?.Employee?.FullName,
                    AccessPoint = accessPoint.Name,
                    MovementType = accessLog.MovementType.ToString(),
                    OccurredAt = accessLog.OccurredAt
                };
            }, "Solicitud de acceso procesada.");
        }

        private async Task<string?> GetRejectionReasonAsync(Credential? credential, AccessPoint accessPoint, MovementType movementType)
        {
            if (credential is null)
            {
                return "La credencial no existe.";
            }

            if (credential.Status == CredentialStatus.Blocked)
            {
                return "La credencial está bloqueada.";
            }

            if (credential.Status == CredentialStatus.Revoked)
            {
                return "La credencial está revocada.";
            }

            if (!credential.Employee.IsActive)
            {
                return "El empleado no se encuentra activo.";
            }

            if (!accessPoint.IsEnabled)
            {
                return "El punto de acceso está deshabilitado.";
            }

            MovementType? lastMovement = await _accessLogsRepository.GetLastAuthorizedMovementAsync(credential.EmployeeId, accessPoint.Id);

            if (movementType == MovementType.Entry && lastMovement == MovementType.Entry)
            {
                return "El empleado ya tiene una entrada registrada en este punto sin su salida.";
            }

            if (movementType == MovementType.Exit && lastMovement != MovementType.Entry)
            {
                return "No hay una entrada previa en este punto para registrar la salida.";
            }

            return null;
        }
    }
}
