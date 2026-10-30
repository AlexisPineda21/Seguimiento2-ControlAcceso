using AccessControl.Domain.Entities.Credentials;
using System.ComponentModel.DataAnnotations;

namespace AccessControl.Api.DTOs.Credentials
{
    public sealed class AssignCredentialDTO
    {
        [Required]
        public Guid EmployeeId { get; set; }

        [Required]
        [StringLength(Credential.CodeMaxLength, MinimumLength = Credential.CodeMinLength)]
        public string Code { get; set; } = string.Empty;
    }
}
