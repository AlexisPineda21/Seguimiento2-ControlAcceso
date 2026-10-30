using AccessControl.Domain.Entities.AccessLogs;
using AccessControl.Domain.Entities.Credentials;
using System.ComponentModel.DataAnnotations;

namespace AccessControl.Api.DTOs.AccessLogs
{
    public sealed class RegisterAccessDTO
    {
        [Required]
        [StringLength(Credential.CodeMaxLength)]
        public string CredentialCode { get; set; } = string.Empty;

        [Required]
        public Guid AccessPointId { get; set; }

        [Required]
        [EnumDataType(typeof(MovementType))]
        public MovementType MovementType { get; set; }
    }
}
