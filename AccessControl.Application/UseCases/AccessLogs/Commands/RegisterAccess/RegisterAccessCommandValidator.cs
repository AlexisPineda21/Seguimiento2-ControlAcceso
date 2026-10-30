using AccessControl.Domain.Entities.Credentials;
using FluentValidation;

namespace AccessControl.Application.UseCases.AccessLogs.Commands.RegisterAccess
{
    public sealed class RegisterAccessCommandValidator : AbstractValidator<RegisterAccessCommand>
    {
        public RegisterAccessCommandValidator()
        {
            RuleFor(command => command.CredentialCode)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("El código de la credencial es requerido.")
                .MaximumLength(Credential.CodeMaxLength).WithMessage($"El código de la credencial debe tener máximo {Credential.CodeMaxLength} caracteres.");

            RuleFor(command => command.AccessPointId)
                .NotEmpty().WithMessage("El punto de acceso es requerido.");

            RuleFor(command => command.MovementType)
                .IsInEnum().WithMessage("El tipo de movimiento debe ser Entry o Exit.");
        }
    }
}
