using AccessControl.Application.Contracts.Repositories;
using AccessControl.Application.Utilities.Pagination;
using AccessControl.Domain.Entities.AccessPoints;
using AccessControl.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace AccessControl.Persistence.Repositories
{
    public class AccessPointsRepository : Repository<AccessPoint>, IAccessPointsRepository
    {
        private readonly DataContext _context;

        public AccessPointsRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PaginationResponse<AccessPoint>> GetPagedListAsync(PaginationRequest pagination,
                                                                             bool? isEnabled,
                                                                             AccessPointType? type,
                                                                             CancellationToken cancellationToken = default)
        {
            IQueryable<AccessPoint> query = _context.AccessPoints.AsNoTracking();

            if (isEnabled.HasValue)
            {
                query = query.Where(accessPoint => accessPoint.IsEnabled == isEnabled.Value);
            }

            if (type.HasValue)
            {
                query = query.Where(accessPoint => accessPoint.Type == type.Value);
            }

            query = query.OrderBy(accessPoint => accessPoint.Name);

            return await query.ToPagedListAsync(pagination, cancellationToken);
        }

        public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.AccessPoints.AnyAsync(accessPoint => accessPoint.Name == name, cancellationToken);
        }
    }
}
