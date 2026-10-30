using AccessControl.Application.Contracts.Repositories;
using AccessControl.Application.UseCases.Common;
using AccessControl.Application.Utilities.Mediator;
using AccessControl.Application.Utilities.Pagination;
using AccessControl.Application.Utilities.Results;
using AccessControl.Domain.Entities.AccessLogs;

namespace AccessControl.Application.UseCases.AccessLogs.Queries.GetAccessHistory
{
    public sealed class GetAccessHistoryUseCase : BaseHandler, IRequestHandler<GetAccessHistoryQuery, Result<PaginationResponse<AccessLogListItemDTO>>>
    {
        private readonly IAccessLogsRepository _accessLogsRepository;

        public GetAccessHistoryUseCase(IAccessLogsRepository accessLogsRepository)
        {
            _accessLogsRepository = accessLogsRepository;
        }

        public Task<Result<PaginationResponse<AccessLogListItemDTO>>> Handle(GetAccessHistoryQuery query)
        {
            return ExecuteAsync(async () =>
            {
                PaginationRequest pagination = new(query.PageNumber, query.PageSize);

                DateTime? from = query.FromDate?.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc);
                DateTime? to = query.ToDate?.AddDays(1).ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc);

                PaginationResponse<AccessLog> accessLogs = await _accessLogsRepository.GetHistoryAsync(pagination,
                                                                                                       query.EmployeeId,
                                                                                                       query.AccessPointId,
                                                                                                       from,
                                                                                                       to,
                                                                                                       query.Result);

                return accessLogs.Map(accessLog => accessLog.ToListItemDto());
            });
        }
    }
}
