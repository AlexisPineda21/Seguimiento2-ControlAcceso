using AccessControl.Application.Contracts.Repositories;
using AccessControl.Application.Exceptions;
using AccessControl.Application.UseCases.Common;
using AccessControl.Application.Utilities.Mediator;
using AccessControl.Application.Utilities.Results;
using AccessControl.Domain.Entities.Credentials;

namespace AccessControl.Application.UseCases.Credentials.Queries.GetCredentialByCode
{
    public sealed class GetCredentialByCodeUseCase : BaseHandler, IRequestHandler<GetCredentialByCodeQuery, Result<CredentialDetailsDTO>>
    {
        private readonly ICredentialsRepository _credentialsRepository;

        public GetCredentialByCodeUseCase(ICredentialsRepository credentialsRepository)
        {
            _credentialsRepository = credentialsRepository;
        }

        public Task<Result<CredentialDetailsDTO>> Handle(GetCredentialByCodeQuery query)
        {
            return ExecuteAsync(async () =>
            {
                Credential? credential = await _credentialsRepository.GetByCodeAsync(Credential.NormalizeCode(query.Code));

                if (credential is null)
                {
                    throw new NotFoundException("No existe una credencial con ese código.");
                }

                return credential.ToDetailsDto();
            });
        }
    }
}
