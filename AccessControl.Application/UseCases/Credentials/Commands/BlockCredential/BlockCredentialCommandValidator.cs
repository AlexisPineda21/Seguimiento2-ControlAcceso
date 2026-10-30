using AccessControl.Domain.Entities.Credentials;
using FluentValidation;

namespace AccessControl.Application.UseCases.Credentials.Commands.BlockCredential
{
    public sealed class BlockCredentialCommandValidator : AbstractValidator<BlockCredentialCommand>
    {
        public BlockCredentialCommandValidator()
        {
            RuleFor(command => command.Id)
                .NotEmpty().WithMessage("El Id de la credencial es requerido.");

            RuleFor(command => command.Reason)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("El motivo del bloqueo es requerido.")
                .MaximumLength(Credential.ReasonMaxLength).WithMessage($"El motivo debe tener máximo {Credential.ReasonMaxLength} caracteres.");
        }
    }
}
