using AccessControl.Domain.Exceptions;

namespace AccessControl.Domain.Entities.AccessPoints
{
    public sealed class AccessPoint
    {
        public const int NameMinLength = 3;
        public const int NameMaxLength = 100;
        public const int DescriptionMaxLength = 500;

        public Guid Id { get; private set; }
        public string Name { get; private set; } = null!;
        public string? Description { get; private set; }
        public AccessPointType Type { get; private set; }
        public bool IsEnabled { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private AccessPoint()
        {

        }

        public AccessPoint(string name, AccessPointType type, string? description = null)
        {
            ApplyNameRules(name);
            ApplyTypeRules(type);
            ApplyDescriptionRules(description);

            Id = Guid.CreateVersion7();
            Name = name.Trim();
            Type = type;
            Description = description?.Trim();
            IsEnabled = true;
            CreatedAt = DateTime.UtcNow;
        }

        public void Enable()
        {
            if (IsEnabled)
            {
                throw new BussinesRuleException("El punto de acceso ya se encuentra habilitado.");
            }

            IsEnabled = true;
        }

        public void Disable()
        {
            if (!IsEnabled)
            {
                throw new BussinesRuleException("El punto de acceso ya se encuentra deshabilitado.");
            }

            IsEnabled = false;
        }

        private static void ApplyNameRules(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new BussinesRuleException("El nombre del punto de acceso es requerido.");
            }

            if (name.Trim().Length < NameMinLength || name.Trim().Length > NameMaxLength)
            {
                throw new BussinesRuleException($"El nombre del punto de acceso debe tener entre {NameMinLength} y {NameMaxLength} caracteres.");
            }
        }

        private static void ApplyTypeRules(AccessPointType type)
        {
            if (!Enum.IsDefined(type))
            {
                throw new BussinesRuleException("El tipo de punto de acceso no es válido.");
            }
        }

        private static void ApplyDescriptionRules(string? description)
        {
            if (description is not null && description.Trim().Length > DescriptionMaxLength)
            {
                throw new BussinesRuleException($"La descripción del punto de acceso debe tener máximo {DescriptionMaxLength} caracteres.");
            }
        }
    }
}
