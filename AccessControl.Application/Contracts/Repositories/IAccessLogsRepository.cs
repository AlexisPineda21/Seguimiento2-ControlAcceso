using AccessControl.Domain.Entities.AccessLogs;

namespace AccessControl.Application.Contracts.Repositories
{
    public interface IAccessLogsRepository : IRepository<AccessLog>
    {
        Task<MovementType?> GetLastAuthorizedMovementAsync(Guid employeeId,
                                                           Guid accessPointId,
                                                           CancellationToken cancellationToken = default);
    }
}
