using AccessControl.Application.Contracts.Persistence;
using AccessControl.Application.Contracts.Repositories;
using AccessControl.Application.Exceptions;
using AccessControl.Application.UseCases.Common;
using AccessControl.Application.Utilities.Mediator;
using AccessControl.Application.Utilities.Results;
using AccessControl.Domain.Entities.Employees;
using AccessControl.Domain.Entities.Employees.ValueObjects;

namespace AccessControl.Application.UseCases.Employees.Commands.CreateEmployee
{
    public sealed class CreateEmployeeUseCase : BaseHandler, IRequestHandler<CreateEmployeeCommand, Result<Guid>>
    {
        private readonly IEmployeesRepository _employeesRepository;
        private readonly IAreasRepository _areasRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateEmployeeUseCase(IEmployeesRepository employeesRepository,
                                     IAreasRepository areasRepository,
                                     IUnitOfWork unitOfWork)
        {
            _employeesRepository = employeesRepository;
            _areasRepository = areasRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<Result<Guid>> Handle(CreateEmployeeCommand command)
        {
            return ExecuteAsync(async () =>
            {
                DocumentNumber documentNumber = new(command.DocumentNumber);
                Email email = new(command.Email);

                if (!await _areasRepository.ExistsAsync(command.AreaId))
                {
                    throw new NotFoundException("El área indicada no existe.");
                }

                if (await _employeesRepository.ExistsByDocumentNumberAsync(documentNumber))
                {
                    throw new ConflictException("Ya existe un empleado con ese número de identificación.");
                }

                if (await _employeesRepository.ExistsByEmailAsync(email))
                {
                    throw new ConflictException("Ya existe un empleado con ese correo electrónico.");
                }

                Employee employee = new(documentNumber,
                                        command.FirstName,
                                        command.LastName,
                                        email,
                                        command.Position,
                                        command.AreaId);

                await _employeesRepository.CreateAsync(employee);
                await _unitOfWork.CommitAsync();

                return employee.Id;
            }, "Empleado registrado correctamente.");
        }
    }
}
