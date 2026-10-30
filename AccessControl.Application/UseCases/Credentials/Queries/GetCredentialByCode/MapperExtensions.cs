using AccessControl.Domain.Entities.Credentials;

namespace AccessControl.Application.UseCases.Credentials.Queries.GetCredentialByCode
{
    public static class MapperExtensions
    {
        public static CredentialDetailsDTO ToDetailsDto(this Credential credential)
        {
            return new CredentialDetailsDTO
            {
                Id = credential.Id,
                Code = credential.Code,
                Status = credential.Status.ToString(),
                CanBeUsed = credential.CanBeUsed,
                StatusReason = credential.StatusReason,
                EmployeeId = credential.EmployeeId,
                EmployeeName = credential.Employee.FullName,
                EmployeeDocumentNumber = credential.Employee.DocumentNumber.Value,
                EmployeeIsActive = credential.Employee.IsActive,
                IssuedAt = credential.IssuedAt,
                BlockedAt = credential.BlockedAt,
                RevokedAt = credential.RevokedAt
            };
        }
    }
}
