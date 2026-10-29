using AccessControl.Application.Utilities.Pagination;
using AccessControl.Domain.Entities.AccessPoints;

namespace AccessControl.Application.Contracts.Repositories
{
    public interface IAccessPointsRepository : IRepository<AccessPoint>
    {
        Task<PaginationResponse<AccessPoint>> GetPagedListAsync(PaginationRequest pagination,
                                                                bool? isEnabled,
                                                                AccessPointType? type,
                                                                CancellationToken cancellationToken = default);

        Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);
    }
}
