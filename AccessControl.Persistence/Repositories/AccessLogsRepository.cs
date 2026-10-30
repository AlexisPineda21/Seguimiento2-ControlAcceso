using AccessControl.Application.Contracts.Repositories;
using AccessControl.Domain.Entities.AccessLogs;
using Microsoft.EntityFrameworkCore;

namespace AccessControl.Persistence.Repositories
{
    public class AccessLogsRepository : Repository<AccessLog>, IAccessLogsRepository
    {
        private readonly DataContext _context;

        public AccessLogsRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<MovementType?> GetLastAuthorizedMovementAsync(Guid employeeId,
                                                                        Guid accessPointId,
                                                                        CancellationToken cancellationToken = default)
        {
            return await _context.AccessLogs.AsNoTracking()
                                            .Where(accessLog => accessLog.EmployeeId == employeeId &&
                                                                accessLog.AccessPointId == accessPointId &&
                                                                accessLog.Result == AccessResult.Authorized)
                                            .OrderByDescending(accessLog => accessLog.OccurredAt)
                                            .Select(accessLog => (MovementType?)accessLog.MovementType)
                                            .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
