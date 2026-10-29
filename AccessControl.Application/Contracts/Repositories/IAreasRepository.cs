using AccessControl.Domain.Entities.Areas;

namespace AccessControl.Application.Contracts.Repositories
{
    public interface IAreasRepository : IRepository<Area>
    {
        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
