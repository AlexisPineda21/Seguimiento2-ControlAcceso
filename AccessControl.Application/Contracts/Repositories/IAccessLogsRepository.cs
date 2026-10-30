using AccessControl.Application.Utilities.Pagination;
using AccessControl.Domain.Entities.AccessLogs;

namespace AccessControl.Application.Contracts.Repositories
{
    public interface IAccessLogsRepository : IRepository<AccessLog>
    {
        Task<MovementType?> GetLastAuthorizedMovementAsync(Guid employeeId,
                                                           Guid accessPointId,
                                                           CancellationToken cancellationToken = default);

        Task<PaginationResponse<AccessLog>> GetHistoryAsync(PaginationRequest pagination,
                                                            Guid? employeeId,
                                                            Guid? accessPointId,
                                                            DateTime? from,
                                                            DateTime? to,
                                                            AccessResult? result,
                                                            CancellationToken cancellationToken = default);
    }
}
