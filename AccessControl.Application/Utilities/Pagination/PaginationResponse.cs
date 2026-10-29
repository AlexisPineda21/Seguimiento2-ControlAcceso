namespace AccessControl.Application.Utilities.Pagination
{
    public class PaginationResponse<T>
    {
        public required IReadOnlyList<T> Items { get; init; }
        public required int TotalCount { get; init; }
        public required int PageNumber { get; init; }
        public required int PageSize { get; init; }

        public int TotalPages => PageSize <= 0 ? 0 : (int)Math.Ceiling(TotalCount / (double)PageSize);
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;

        public static PaginationResponse<T> Create(IReadOnlyList<T> items, int totalCount, PaginationRequest request)
        {
            return new PaginationResponse<T>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }

        public PaginationResponse<TResult> Map<TResult>(Func<T, TResult> selector)
        {
            return new PaginationResponse<TResult>
            {
                Items = Items.Select(selector).ToList(),
                TotalCount = TotalCount,
                PageNumber = PageNumber,
                PageSize = PageSize
            };
        }
    }
}
