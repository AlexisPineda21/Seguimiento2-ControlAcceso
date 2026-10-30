using AccessControl.Application.Contracts.Repositories;
using AccessControl.Domain.Entities.Credentials;
using Microsoft.EntityFrameworkCore;

namespace AccessControl.Persistence.Repositories
{
    public class CredentialsRepository : Repository<Credential>, ICredentialsRepository
    {
        private readonly DataContext _context;

        public CredentialsRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Credential?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
        {
            return await _context.Credentials.Include(credential => credential.Employee)
                                             .FirstOrDefaultAsync(credential => credential.Code == code, cancellationToken);
        }

        public async Task<IReadOnlyList<Credential>> GetByEmployeeAsync(Guid employeeId, CancellationToken cancellationToken = default)
        {
            return await _context.Credentials.AsNoTracking()
                                             .Where(credential => credential.EmployeeId == employeeId)
                                             .OrderByDescending(credential => credential.IssuedAt)
                                             .ToListAsync(cancellationToken);
        }

        public async Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default)
        {
            return await _context.Credentials.AnyAsync(credential => credential.Code == code, cancellationToken);
        }

        public async Task<bool> HasCurrentCredentialAsync(Guid employeeId, CancellationToken cancellationToken = default)
        {
            return await _context.Credentials.AnyAsync(credential => credential.EmployeeId == employeeId &&
                                                                     credential.Status != CredentialStatus.Revoked,
                                                       cancellationToken);
        }
    }
}
