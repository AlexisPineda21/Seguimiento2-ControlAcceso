using AccessControl.Application.Utilities.Mediator;
using AccessControl.Application.Utilities.Results;

namespace AccessControl.Application.UseCases.Employees.Commands.DeactivateEmployee
{
    public sealed class DeactivateEmployeeCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}
