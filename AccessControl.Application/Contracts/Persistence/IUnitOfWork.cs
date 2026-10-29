namespace AccessControl.Application.Contracts.Persistence
{
    public interface IUnitOfWork
    {
        Task CommitAsync(CancellationToken cancellationToken = default);
    }
}
