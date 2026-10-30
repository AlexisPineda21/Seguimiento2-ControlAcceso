using AccessControl.Application.Utilities.Mediator;
using AccessControl.Application.Utilities.Pagination;
using AccessControl.Application.Utilities.Results;
using AccessControl.Domain.Entities.AccessLogs;

namespace AccessControl.Application.UseCases.AccessLogs.Queries.GetAccessHistory
{
    public sealed class GetAccessHistoryQuery : IRequest<Result<PaginationResponse<AccessLogListItemDTO>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = PaginationRequest.DefaultPageSize;
        public Guid? EmployeeId { get; set; }
        public Guid? AccessPointId { get; set; }
        public DateOnly? FromDate { get; set; }
        public DateOnly? ToDate { get; set; }
        public AccessResult? Result { get; set; }
    }
}
