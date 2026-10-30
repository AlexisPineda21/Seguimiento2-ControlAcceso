using AccessControl.Application.Utilities.Mediator;
using AccessControl.Application.Utilities.Results;

namespace AccessControl.Application.UseCases.Credentials.Commands.UnblockCredential
{
    public sealed class UnblockCredentialCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}
