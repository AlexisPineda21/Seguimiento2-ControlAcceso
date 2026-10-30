using AccessControl.Application.Contracts.Persistence;
using AccessControl.Application.Contracts.Repositories;
using AccessControl.Application.Exceptions;
using AccessControl.Application.UseCases.Common;
using AccessControl.Application.Utilities.Mediator;
using AccessControl.Application.Utilities.Results;
using AccessControl.Domain.Entities.Credentials;

namespace AccessControl.Application.UseCases.Credentials.Commands.RevokeCredential
{
    public sealed class RevokeCredentialUseCase : BaseHandler, IRequestHandler<RevokeCredentialCommand, Result>
    {
        private readonly ICredentialsRepository _credentialsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RevokeCredentialUseCase(ICredentialsRepository credentialsRepository, IUnitOfWork unitOfWork)
        {
            _credentialsRepository = credentialsRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<Result> Handle(RevokeCredentialCommand command)
        {
            return ExecuteAsync(async () =>
            {
                Credential? credential = await _credentialsRepository.GetByIdAsync(command.Id);

                if (credential is null)
                {
                    throw new NotFoundException("La credencial no existe.");
                }

                credential.Revoke(command.Reason);

                await _unitOfWork.CommitAsync();
            }, "Credencial revocada correctamente.");
        }
    }
}
