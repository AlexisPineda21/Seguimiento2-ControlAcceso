using AccessControl.Application.Contracts.Persistence;
using AccessControl.Application.Contracts.Repositories;
using AccessControl.Application.Exceptions;
using AccessControl.Application.UseCases.Common;
using AccessControl.Application.Utilities.Mediator;
using AccessControl.Application.Utilities.Results;
using AccessControl.Domain.Entities.Employees;
using AccessControl.Domain.Entities.Employees.ValueObjects;

namespace AccessControl.Application.UseCases.Employees.Commands.UpdateEmployee
{
    public sealed class UpdateEmployeeUseCase : BaseHandler, IRequestHandler<UpdateEmployeeCommand, Result>
    {
        private readonly IEmployeesRepository _employeesRepository;
        private readonly IAreasRepository _areasRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateEmployeeUseCase(IEmployeesRepository employeesRepository,
                                     IAreasRepository areasRepository,
                                     IUnitOfWork unitOfWork)
        {
            _employeesRepository = employeesRepository;
            _areasRepository = areasRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<Result> Handle(UpdateEmployeeCommand command)
        {
            return ExecuteAsync(async () =>
            {
                Employee? employee = await _employeesRepository.GetByIdAsync(command.Id);

                if (employee is null)
                {
                    throw new NotFoundException("El empleado no existe.");
                }

                if (!await _areasRepository.ExistsAsync(command.AreaId))
                {
                    throw new NotFoundException("El área indicada no existe.");
                }

                Email email = new(command.Email);

                if (await _employeesRepository.ExistsByEmailAsync(email, employee.Id))
                {
                    throw new ConflictException("Ya existe otro empleado con ese correo electrónico.");
                }

                employee.Update(command.FirstName,
                                command.LastName,
                                email,
                                command.Position,
                                command.AreaId);

                await _employeesRepository.UpdateAsync(employee);
                await _unitOfWork.CommitAsync();
            }, "Empleado actualizado correctamente.");
        }
    }
}
