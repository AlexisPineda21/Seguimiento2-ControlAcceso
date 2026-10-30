using AccessControl.Domain.Entities.Credentials;
using System.ComponentModel.DataAnnotations;

namespace AccessControl.Api.DTOs.Credentials
{
    public sealed class CredentialReasonDTO
    {
        [Required]
        [StringLength(Credential.ReasonMaxLength)]
        public string Reason { get; set; } = string.Empty;
    }
}
