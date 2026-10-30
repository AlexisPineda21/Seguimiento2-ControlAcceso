using AccessControl.Application.Contracts.Persistence;
using AccessControl.Application.Contracts.Repositories;
using AccessControl.Application.Exceptions;
using AccessControl.Application.UseCases.Common;
using AccessControl.Application.Utilities.Mediator;
using AccessControl.Application.Utilities.Results;
using AccessControl.Domain.Entities.Credentials;
using AccessControl.Domain.Entities.Employees;
using AccessControl.Domain.Exceptions;

namespace AccessControl.Application.UseCases.Credentials.Commands.AssignCredential
{
    public sealed class AssignCredentialUseCase : BaseHandler, IRequestHandler<AssignCredentialCommand, Result<Guid>>
    {
        private readonly ICredentialsRepository _credentialsRepository;
        private readonly IEmployeesRepository _employeesRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AssignCredentialUseCase(ICredentialsRepository credentialsRepository,
                                       IEmployeesRepository employeesRepository,
                                       IUnitOfWork unitOfWork)
        {
            _credentialsRepository = credentialsRepository;
            _employeesRepository = employeesRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<Result<Guid>> Handle(AssignCredentialCommand command)
        {
            return ExecuteAsync(async () =>
            {
                Employee? employee = await _employeesRepository.GetByIdAsync(command.EmployeeId);

                if (employee is null)
                {
                    throw new NotFoundException("El empleado no existe.");
                }

                if (!employee.IsActive)
                {
                    throw new BussinesRuleException("No se puede asignar una credencial a un empleado inactivo.");
                }

                string code = Credential.NormalizeCode(command.Code);

                if (await _credentialsRepository.ExistsByCodeAsync(code))
                {
                    throw new ConflictException("Ya existe una credencial con ese código.");
                }

                if (await _credentialsRepository.HasCurrentCredentialAsync(employee.Id))
                {
                    throw new ConflictException("El empleado ya tiene una credencial vigente. Debe revocarla antes de asignar una nueva.");
                }

                Credential credential = new(code, employee.Id);

                await _credentialsRepository.CreateAsync(credential);
                await _unitOfWork.CommitAsync();

                return credential.Id;
            }, "Credencial asignada correctamente.");
        }
    }
}
