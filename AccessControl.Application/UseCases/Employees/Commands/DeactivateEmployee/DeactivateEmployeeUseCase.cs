using AccessControl.Application.Contracts.Persistence;
using AccessControl.Application.Contracts.Repositories;
using AccessControl.Application.Exceptions;
using AccessControl.Application.UseCases.Common;
using AccessControl.Application.Utilities.Mediator;
using AccessControl.Application.Utilities.Results;
using AccessControl.Domain.Entities.Employees;

namespace AccessControl.Application.UseCases.Employees.Commands.DeactivateEmployee
{
    public sealed class DeactivateEmployeeUseCase : BaseHandler, IRequestHandler<DeactivateEmployeeCommand, Result>
    {
        private readonly IEmployeesRepository _employeesRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeactivateEmployeeUseCase(IEmployeesRepository employeesRepository, IUnitOfWork unitOfWork)
        {
            _employeesRepository = employeesRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<Result> Handle(DeactivateEmployeeCommand command)
        {
            return ExecuteAsync(async () =>
            {
                Employee? employee = await _employeesRepository.GetByIdAsync(command.Id);

                if (employee is null)
                {
                    throw new NotFoundException("El empleado no existe.");
                }

                employee.Deactivate();

                await _unitOfWork.CommitAsync();
            }, "Empleado desactivado correctamente.");
        }
    }
}
