using AccessControl.Application.Contracts.Repositories;
using AccessControl.Application.Exceptions;
using AccessControl.Application.UseCases.Common;
using AccessControl.Application.Utilities.Mediator;
using AccessControl.Application.Utilities.Results;
using AccessControl.Domain.Entities.Credentials;
using AccessControl.Domain.Entities.Employees;

namespace AccessControl.Application.UseCases.Credentials.Queries.GetCredentialsByEmployee
{
    public sealed class GetCredentialsByEmployeeUseCase : BaseHandler, IRequestHandler<GetCredentialsByEmployeeQuery, Result<IReadOnlyList<CredentialListItemDTO>>>
    {
        private readonly ICredentialsRepository _credentialsRepository;
        private readonly IEmployeesRepository _employeesRepository;

        public GetCredentialsByEmployeeUseCase(ICredentialsRepository credentialsRepository, IEmployeesRepository employeesRepository)
        {
            _credentialsRepository = credentialsRepository;
            _employeesRepository = employeesRepository;
        }

        public Task<Result<IReadOnlyList<CredentialListItemDTO>>> Handle(GetCredentialsByEmployeeQuery query)
        {
            return ExecuteAsync<IReadOnlyList<CredentialListItemDTO>>(async () =>
            {
                Employee? employee = await _employeesRepository.GetByIdAsync(query.EmployeeId);

                if (employee is null)
                {
                    throw new NotFoundException("El empleado no existe.");
                }

                IReadOnlyList<Credential> credentials = await _credentialsRepository.GetByEmployeeAsync(employee.Id);

                return credentials.Select(credential => new CredentialListItemDTO
                                  {
                                      Id = credential.Id,
                                      Code = credential.Code,
                                      Status = credential.Status.ToString(),
                                      StatusReason = credential.StatusReason,
                                      IssuedAt = credential.IssuedAt
                                  })
                                  .ToList();
            });
        }
    }
}
