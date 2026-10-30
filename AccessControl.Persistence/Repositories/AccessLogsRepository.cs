using AccessControl.Application.Contracts.Repositories;
using AccessControl.Application.Utilities.Pagination;
using AccessControl.Domain.Entities.AccessLogs;
using AccessControl.Persistence.Extensions;
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

        public async Task<PaginationResponse<AccessLog>> GetHistoryAsync(PaginationRequest pagination,
                                                                         Guid? employeeId,
                                                                         Guid? accessPointId,
                                                                         DateTime? from,
                                                                         DateTime? to,
                                                                         AccessResult? result,
                                                                         CancellationToken cancellationToken = default)
        {
            IQueryable<AccessLog> query = _context.AccessLogs.AsNoTracking()
                                                             .Include(accessLog => accessLog.Employee)
                                                             .Include(accessLog => accessLog.AccessPoint);

            if (employeeId.HasValue)
            {
                query = query.Where(accessLog => accessLog.EmployeeId == employeeId.Value);
            }

            if (accessPointId.HasValue)
            {
                query = query.Where(accessLog => accessLog.AccessPointId == accessPointId.Value);
            }

            if (from.HasValue)
            {
                query = query.Where(accessLog => accessLog.OccurredAt >= from.Value);
            }

            if (to.HasValue)
            {
                query = query.Where(accessLog => accessLog.OccurredAt < to.Value);
            }

            if (result.HasValue)
            {
                query = query.Where(accessLog => accessLog.Result == result.Value);
            }

            query = query.OrderByDescending(accessLog => accessLog.OccurredAt);

            return await query.ToPagedListAsync(pagination, cancellationToken);
        }
    }
}
