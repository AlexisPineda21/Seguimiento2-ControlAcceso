using AccessControl.Domain.Entities.Employees;
using AccessControl.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace AccessControl.Domain.Entities.Credentials
{
    public sealed class Credential
    {
        public const int CodeMinLength = 4;
        public const int CodeMaxLength = 20;
        public const int ReasonMaxLength = 250;

        private static readonly Regex CodeRegex = new(@"^[A-Z0-9-]+$", RegexOptions.Compiled);

        public Guid Id { get; private set; }
        public string Code { get; private set; } = null!;
        public Guid EmployeeId { get; private set; }
        public Employee Employee { get; private set; } = null!;
        public CredentialStatus Status { get; private set; }
        public string? StatusReason { get; private set; }
        public DateTime IssuedAt { get; private set; }
        public DateTime? BlockedAt { get; private set; }
        public DateTime? RevokedAt { get; private set; }

        public bool CanBeUsed => Status == CredentialStatus.Active;

        private Credential()
        {

        }

        public Credential(string code, Guid employeeId)
        {
            string normalizedCode = NormalizeCode(code);

            ApplyCodeRules(normalizedCode);
            ApplyEmployeeIdRules(employeeId);

            Id = Guid.CreateVersion7();
            Code = normalizedCode;
            EmployeeId = employeeId;
            Status = CredentialStatus.Active;
            IssuedAt = DateTime.UtcNow;
        }

        public void Block(string reason)
        {
            if (Status != CredentialStatus.Active)
            {
                throw new BussinesRuleException("Solo se puede bloquear una credencial activa.");
            }

            ApplyReasonRules(reason);

            Status = CredentialStatus.Blocked;
            StatusReason = reason.Trim();
            BlockedAt = DateTime.UtcNow;
        }

        public void Unblock()
        {
            if (Status != CredentialStatus.Blocked)
            {
                throw new BussinesRuleException("Solo se puede desbloquear una credencial bloqueada.");
            }

            Status = CredentialStatus.Active;
            StatusReason = null;
            BlockedAt = null;
        }

        public void Revoke(string reason)
        {
            if (Status == CredentialStatus.Revoked)
            {
                throw new BussinesRuleException("La credencial ya se encuentra revocada.");
            }

            ApplyReasonRules(reason);

            Status = CredentialStatus.Revoked;
            StatusReason = reason.Trim();
            RevokedAt = DateTime.UtcNow;
        }

        public static string NormalizeCode(string code)
        {
            return string.IsNullOrWhiteSpace(code) ? string.Empty : code.Trim().ToUpperInvariant();
        }

        private static void ApplyCodeRules(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new BussinesRuleException("El código de la credencial es requerido.");
            }

            if (code.Length < CodeMinLength || code.Length > CodeMaxLength)
            {
                throw new BussinesRuleException($"El código de la credencial debe tener entre {CodeMinLength} y {CodeMaxLength} caracteres.");
            }

            if (!CodeRegex.IsMatch(code))
            {
                throw new BussinesRuleException("El código de la credencial solo puede contener letras, números y guiones.");
            }
        }

        private static void ApplyEmployeeIdRules(Guid employeeId)
        {
            if (employeeId == Guid.Empty)
            {
                throw new BussinesRuleException("El empleado de la credencial es requerido.");
            }
        }

        private static void ApplyReasonRules(string reason)
        {
            if (string.IsNullOrWhiteSpace(reason))
            {
                throw new BussinesRuleException("El motivo es requerido.");
            }

            if (reason.Trim().Length > ReasonMaxLength)
            {
                throw new BussinesRuleException($"El motivo debe tener máximo {ReasonMaxLength} caracteres.");
            }
        }
    }
}
