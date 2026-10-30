using AccessControl.Application.Utilities.Mediator;
using AccessControl.Application.Utilities.Results;
using AccessControl.Domain.Entities.AccessLogs;

namespace AccessControl.Application.UseCases.AccessLogs.Commands.RegisterAccess
{
    public sealed class RegisterAccessCommand : IRequest<Result<RegisterAccessResultDTO>>
    {
        public string CredentialCode { get; set; } = string.Empty;
        public Guid AccessPointId { get; set; }
        public MovementType MovementType { get; set; }
    }
}
