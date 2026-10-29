using AccessControl.Application.Contracts.Repositories;
using AccessControl.Domain.Entities.Areas;
using Microsoft.EntityFrameworkCore;

namespace AccessControl.Persistence.Repositories
{
    public class AreasRepository : Repository<Area>, IAreasRepository
    {
        private readonly DataContext _context;

        public AreasRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Areas.AnyAsync(area => area.Id == id, cancellationToken);
        }
    }
}
