using AccessControl.Domain.Entities.Credentials;
using FluentValidation;

namespace AccessControl.Application.UseCases.Credentials.Commands.AssignCredential
{
    public sealed class AssignCredentialCommandValidator : AbstractValidator<AssignCredentialCommand>
    {
        public AssignCredentialCommandValidator()
        {
            RuleFor(command => command.EmployeeId)
                .NotEmpty().WithMessage("El empleado es requerido.");

            RuleFor(command => command.Code)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("El código de la credencial es requerido.")
                .Length(Credential.CodeMinLength, Credential.CodeMaxLength)
                .WithMessage($"El código debe tener entre {Credential.CodeMinLength} y {Credential.CodeMaxLength} caracteres.")
                .Matches("^[A-Za-z0-9-]+$").WithMessage("El código solo puede contener letras, números y guiones.");
        }
    }
}
