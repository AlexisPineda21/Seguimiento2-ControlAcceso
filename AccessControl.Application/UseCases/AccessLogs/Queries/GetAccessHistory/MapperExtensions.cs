using AccessControl.Domain.Entities.AccessLogs;

namespace AccessControl.Application.UseCases.AccessLogs.Queries.GetAccessHistory
{
    public static class MapperExtensions
    {
        public static AccessLogListItemDTO ToListItemDto(this AccessLog accessLog)
        {
            return new AccessLogListItemDTO
            {
                Id = accessLog.Id,
                OccurredAt = accessLog.OccurredAt,
                CredentialCode = accessLog.CredentialCode,
                EmployeeId = accessLog.EmployeeId,
                EmployeeName = accessLog.Employee?.FullName,
                AccessPointId = accessLog.AccessPointId,
                AccessPoint = accessLog.AccessPoint?.Name ?? string.Empty,
                MovementType = accessLog.MovementType.ToString(),
                Result = accessLog.Result.ToString(),
                RejectionReason = accessLog.RejectionReason
            };
        }
    }
}
