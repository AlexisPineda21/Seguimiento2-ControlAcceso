using AccessControl.Application.Utilities.Mediator;
using AccessControl.Application.Utilities.Results;

namespace AccessControl.Application.UseCases.AccessPoints.Queries.GetAccessPointById
{
    public sealed class GetAccessPointByIdQuery : IRequest<Result<AccessPointDetailsDTO>>
    {
        public Guid Id { get; set; }
    }
}
