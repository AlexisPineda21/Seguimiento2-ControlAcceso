using AccessControl.Domain.Entities.Credentials;

namespace AccessControl.Application.Contracts.Repositories
{
    public interface ICredentialsRepository : IRepository<Credential>
    {
        Task<Credential?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Credential>> GetByEmployeeAsync(Guid employeeId, CancellationToken cancellationToken = default);

        Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default);

        Task<bool> HasCurrentCredentialAsync(Guid employeeId, CancellationToken cancellationToken = default);
    }
}
