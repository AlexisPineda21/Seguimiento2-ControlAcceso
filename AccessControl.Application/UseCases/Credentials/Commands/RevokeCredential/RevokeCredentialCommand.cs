using AccessControl.Application.Utilities.Mediator;
using AccessControl.Application.Utilities.Results;

namespace AccessControl.Application.UseCases.Credentials.Commands.RevokeCredential
{
    public sealed class RevokeCredentialCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public string Reason { get; set; } = string.Empty;
    }
}
