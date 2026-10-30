using AccessControl.Domain.Entities.AccessPoints;
using AccessControl.Domain.Entities.Credentials;
using AccessControl.Domain.Entities.Employees;
using AccessControl.Domain.Exceptions;

namespace AccessControl.Domain.Entities.AccessLogs
{
    public sealed class AccessLog
    {
        public const int ReasonMaxLength = 250;

        public Guid Id { get; private set; }
        public string CredentialCode { get; private set; } = null!;
        public Guid? CredentialId { get; private set; }
        public Credential? Credential { get; private set; }
        public Guid? EmployeeId { get; private set; }
        public Employee? Employee { get; private set; }
        public Guid AccessPointId { get; private set; }
        public AccessPoint AccessPoint { get; private set; } = null!;
        public MovementType MovementType { get; private set; }
        public AccessResult Result { get; private set; }
        public string? RejectionReason { get; private set; }
        public DateTime OccurredAt { get; private set; }

        private AccessLog()
        {

        }

        private AccessLog(string credentialCode,
                          Guid? credentialId,
                          Guid? employeeId,
                          Guid accessPointId,
                          MovementType movementType,
                          AccessResult result,
                          string? rejectionReason)
        {
            ApplyCredentialCodeRules(credentialCode);
            ApplyAccessPointIdRules(accessPointId);
            ApplyMovementTypeRules(movementType);

            Id = Guid.CreateVersion7();
            CredentialCode = credentialCode.Trim().ToUpperInvariant();
            CredentialId = credentialId;
            EmployeeId = employeeId;
            AccessPointId = accessPointId;
            MovementType = movementType;
            Result = result;
            RejectionReason = rejectionReason;
            OccurredAt = DateTime.UtcNow;
        }

        public static AccessLog Authorize(Credential credential, Guid accessPointId, MovementType movementType)
        {
            if (credential is null)
            {
                throw new BussinesRuleException("La credencial es requerida para autorizar un acceso.");
            }

            return new AccessLog(credential.Code,
                                 credential.Id,
                                 credential.EmployeeId,
                                 accessPointId,
                                 movementType,
                                 AccessResult.Authorized,
                                 null);
        }

        public static AccessLog Reject(string credentialCode,
                                       Guid accessPointId,
                                       MovementType movementType,
                                       string reason,
                                       Credential? credential = null)
        {
            if (string.IsNullOrWhiteSpace(reason))
            {
                throw new BussinesRuleException("El motivo del rechazo es requerido.");
            }

            string trimmedReason = reason.Trim();

            if (trimmedReason.Length > ReasonMaxLength)
            {
                trimmedReason = trimmedReason[..ReasonMaxLength];
            }

            return new AccessLog(credentialCode,
                                 credential?.Id,
                                 credential?.EmployeeId,
                                 accessPointId,
                                 movementType,
                                 AccessResult.Rejected,
                                 trimmedReason);
        }

        private static void ApplyCredentialCodeRules(string credentialCode)
        {
            if (string.IsNullOrWhiteSpace(credentialCode))
            {
                throw new BussinesRuleException("El código de la credencial es requerido.");
            }
        }

        private static void ApplyAccessPointIdRules(Guid accessPointId)
        {
            if (accessPointId == Guid.Empty)
            {
                throw new BussinesRuleException("El punto de acceso es requerido.");
            }
        }

        private static void ApplyMovementTypeRules(MovementType movementType)
        {
            if (!Enum.IsDefined(movementType))
            {
                throw new BussinesRuleException("El tipo de movimiento no es válido.");
            }
        }
    }
}
