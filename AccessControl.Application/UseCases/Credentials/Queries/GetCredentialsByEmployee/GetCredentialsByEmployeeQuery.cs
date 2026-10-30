using AccessControl.Application.Utilities.Mediator;
using AccessControl.Application.Utilities.Results;

namespace AccessControl.Application.UseCases.Credentials.Queries.GetCredentialsByEmployee
{
    public sealed class GetCredentialsByEmployeeQuery : IRequest<Result<IReadOnlyList<CredentialListItemDTO>>>
    {
        public Guid EmployeeId { get; set; }
    }
}
