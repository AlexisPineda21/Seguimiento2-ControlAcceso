using AccessControl.Application.Utilities.Mediator;
using AccessControl.Application.Utilities.Results;

namespace AccessControl.Application.UseCases.AccessPoints.Commands.EnableAccessPoint
{
    public sealed class EnableAccessPointCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}
