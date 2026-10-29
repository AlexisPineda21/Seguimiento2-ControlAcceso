using AccessControl.Application.UseCases.AccessPoints.Commands.CreateAccessPoint;
using AccessControl.Application.UseCases.AccessPoints.Commands.DisableAccessPoint;
using AccessControl.Application.UseCases.AccessPoints.Commands.EnableAccessPoint;
using AccessControl.Application.UseCases.AccessPoints.Queries.GetAccessPointById;
using AccessControl.Application.UseCases.AccessPoints.Queries.GetAccessPointsList;
using AccessControl.Application.UseCases.Areas.Queries.GetAreasList;
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

            // Validations
            services.AddValidatorsFromAssembly(typeof(ApplicationServicesRegistry).Assembly);

            return services;
        }
    }
}
