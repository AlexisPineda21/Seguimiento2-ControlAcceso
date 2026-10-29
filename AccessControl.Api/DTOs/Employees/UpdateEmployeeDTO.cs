using AccessControl.Domain.Entities.Employees;
using System.ComponentModel.DataAnnotations;

namespace AccessControl.Api.DTOs.Employees
{
    public sealed class UpdateEmployeeDTO
    {
        [Required]
        [StringLength(Employee.NameMaxLength)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(Employee.NameMaxLength)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(Employee.PositionMaxLength)]
        public string Position { get; set; } = string.Empty;

        [Required]
        public Guid AreaId { get; set; }
    }
}
