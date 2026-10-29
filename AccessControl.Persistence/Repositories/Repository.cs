using AccessControl.Application.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AccessControl.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DataContext _context;

        public Repository(DataContext context)
        {
            _context = context;
        }

        public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>().FindAsync([id], cancellationToken);
        }

        public async Task<IReadOnlyList<T>> GetListAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>().ToListAsync(cancellationToken);
        }

        public Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default)
        {
            _context.Add(entity);
            return Task.FromResult(entity);
        }

        public Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            _context.Update(entity);
            return Task.FromResult(entity);
        }
    }
}
