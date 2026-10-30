using AccessControl.Application.UseCases.AccessLogs.Commands.RegisterAccess;
using AccessControl.Application.UseCases.AccessLogs.Queries.GetAccessHistory;
using AccessControl.Application.UseCases.AccessPoints.Commands.CreateAccessPoint;
using AccessControl.Application.UseCases.AccessPoints.Commands.DisableAccessPoint;
using AccessControl.Application.UseCases.AccessPoints.Commands.EnableAccessPoint;
using AccessControl.Application.UseCases.AccessPoints.Queries.GetAccessPointById;
using AccessControl.Application.UseCases.AccessPoints.Queries.GetAccessPointsList;
using AccessControl.Application.UseCases.Areas.Queries.GetAreasList;
using AccessControl.Application.UseCases.Credentials.Commands.AssignCredential;
using AccessControl.Application.UseCases.Credentials.Commands.BlockCredential;
using AccessControl.Application.UseCases.Credentials.Commands.RevokeCredential;
using AccessControl.Application.UseCases.Credentials.Commands.UnblockCredential;
using AccessControl.Application.UseCases.Credentials.Queries.GetCredentialByCode;
using AccessControl.Application.UseCases.Credentials.Queries.GetCredentialsByEmployee;
using AccessControl.Application.UseCases.Employees.Commands.CreateEmployee;
using AccessControl.Application.UseCases.Employees.Commands.DeactivateEmployee;
using AccessControl.Application.UseCases.Employees.Commands.UpdateEmployee;
using AccessControl.Application.UseCases.Employees.Queries.GetEmployeeById;
using AccessControl.Application.UseCases.Employees.Queries.GetEmployeesList;
using AccessControl.Application.Utilities.Mediator;
using AccessControl.Application.Utilities.Pagination;
using AccessControl.Application.Utilities.Results;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace AccessControl.Application
{
    public static class ApplicationServicesRegistry
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Mediator
            services.AddScoped<IMediator, SimpleMediator>();

            // Use Cases
            services.AddScoped<IRequestHandler<GetAreasListQuery, Result<IReadOnlyList<AreaDTO>>>, GetAreasListUseCase>();

            services.AddScoped<IRequestHandler<GetAccessPointsListQuery, Result<PaginationResponse<AccessPointListItemDTO>>>, GetAccessPointsListUseCase>();
            services.AddScoped<IRequestHandler<GetAccessPointByIdQuery, Result<AccessPointDetailsDTO>>, GetAccessPointByIdUseCase>();
            services.AddScoped<IRequestHandler<CreateAccessPointCommand, Result<Guid>>, CreateAccessPointUseCase>();
            services.AddScoped<IRequestHandler<EnableAccessPointCommand, Result>, EnableAccessPointUseCase>();
            services.AddScoped<IRequestHandler<DisableAccessPointCommand, Result>, DisableAccessPointUseCase>();

            services.AddScoped<IRequestHandler<GetEmployeesListQuery, Result<PaginationResponse<EmployeeListItemDTO>>>, GetEmployeesListUseCase>();
            services.AddScoped<IRequestHandler<GetEmployeeByIdQuery, Result<EmployeeDetailsDTO>>, GetEmployeeByIdUseCase>();
            services.AddScoped<IRequestHandler<CreateEmployeeCommand, Result<Guid>>, CreateEmployeeUseCase>();
            services.AddScoped<IRequestHandler<UpdateEmployeeCommand, Result>, UpdateEmployeeUseCase>();
            services.AddScoped<IRequestHandler<DeactivateEmployeeCommand, Result>, DeactivateEmployeeUseCase>();

            services.AddScoped<IRequestHandler<GetCredentialByCodeQuery, Result<CredentialDetailsDTO>>, GetCredentialByCodeUseCase>();
            services.AddScoped<IRequestHandler<GetCredentialsByEmployeeQuery, Result<IReadOnlyList<CredentialListItemDTO>>>, GetCredentialsByEmployeeUseCase>();
            services.AddScoped<IRequestHandler<AssignCredentialCommand, Result<Guid>>, AssignCredentialUseCase>();
            services.AddScoped<IRequestHandler<BlockCredentialCommand, Result>, BlockCredentialUseCase>();
            services.AddScoped<IRequestHandler<UnblockCredentialCommand, Result>, UnblockCredentialUseCase>();
            services.AddScoped<IRequestHandler<RevokeCredentialCommand, Result>, RevokeCredentialUseCase>();

            services.AddScoped<IRequestHandler<RegisterAccessCommand, Result<RegisterAccessResultDTO>>, RegisterAccessUseCase>();
            services.AddScoped<IRequestHandler<GetAccessHistoryQuery, Result<PaginationResponse<AccessLogListItemDTO>>>, GetAccessHistoryUseCase>();

            // Validations
            services.AddValidatorsFromAssembly(typeof(ApplicationServicesRegistry).Assembly);

            return services;
        }
    }
}
