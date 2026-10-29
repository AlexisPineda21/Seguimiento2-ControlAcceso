using AccessControl.Domain.Entities.AccessPoints;
using System.ComponentModel.DataAnnotations;

namespace AccessControl.Api.DTOs.AccessPoints
{
    public sealed class CreateAccessPointDTO
    {
        [Required]
        [StringLength(AccessPoint.NameMaxLength, MinimumLength = AccessPoint.NameMinLength)]
        public string Name { get; set; } = string.Empty;

        [StringLength(AccessPoint.DescriptionMaxLength)]
        public string? Description { get; set; }

        [Required]
        [EnumDataType(typeof(AccessPointType))]
        public AccessPointType Type { get; set; }
    }
}
