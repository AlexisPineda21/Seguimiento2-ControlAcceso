using AccessControl.Domain.Entities.AccessPoints;
using FluentValidation;

namespace AccessControl.Application.UseCases.AccessPoints.Commands.CreateAccessPoint
{
    public sealed class CreateAccessPointCommandValidator : AbstractValidator<CreateAccessPointCommand>
    {
        public CreateAccessPointCommandValidator()
        {
            RuleFor(command => command.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("El nombre del punto de acceso es requerido.")
                .Length(AccessPoint.NameMinLength, AccessPoint.NameMaxLength)
                .WithMessage($"El nombre del punto de acceso debe tener entre {AccessPoint.NameMinLength} y {AccessPoint.NameMaxLength} caracteres.");

            RuleFor(command => command.Description)
                .MaximumLength(AccessPoint.DescriptionMaxLength)
                .WithMessage($"La descripción debe tener máximo {AccessPoint.DescriptionMaxLength} caracteres.");

            RuleFor(command => command.Type)
                .IsInEnum().WithMessage("El tipo de punto de acceso no es válido.");
        }
    }
}
