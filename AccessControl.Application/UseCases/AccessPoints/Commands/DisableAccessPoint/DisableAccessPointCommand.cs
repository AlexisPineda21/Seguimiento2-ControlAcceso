using AccessControl.Application.Utilities.Mediator;
using AccessControl.Application.Utilities.Results;

namespace AccessControl.Application.UseCases.AccessPoints.Commands.DisableAccessPoint
{
    public sealed class DisableAccessPointCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}
