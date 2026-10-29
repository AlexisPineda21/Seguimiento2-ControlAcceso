using AccessControl.Application.Utilities.Pagination;
using Microsoft.EntityFrameworkCore;

namespace AccessControl.Persistence.Extensions
{
    internal static class QueryableExtensions
    {
        public static async Task<PaginationResponse<T>> ToPagedListAsync<T>(this IQueryable<T> query,
                                                                            PaginationRequest request,
                                                                            CancellationToken cancellationToken = default)
        {
            int totalCount = await query.CountAsync(cancellationToken);

            List<T> items = await query.Skip(request.Skip)
                                       .Take(request.PageSize)
                                       .ToListAsync(cancellationToken);

            return PaginationResponse<T>.Create(items, totalCount, request);
        }
    }
}
