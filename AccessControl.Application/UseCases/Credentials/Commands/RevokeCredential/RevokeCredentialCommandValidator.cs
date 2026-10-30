using AccessControl.Domain.Entities.Credentials;
using FluentValidation;

namespace AccessControl.Application.UseCases.Credentials.Commands.RevokeCredential
{
    public sealed class RevokeCredentialCommandValidator : AbstractValidator<RevokeCredentialCommand>
    {
        public RevokeCredentialCommandValidator()
        {
            RuleFor(command => command.Id)
                .NotEmpty().WithMessage("El Id de la credencial es requerido.");

            RuleFor(command => command.Reason)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("El motivo de la revocación es requerido.")
                .MaximumLength(Credential.ReasonMaxLength).WithMessage($"El motivo debe tener máximo {Credential.ReasonMaxLength} caracteres.");
        }
    }
}
