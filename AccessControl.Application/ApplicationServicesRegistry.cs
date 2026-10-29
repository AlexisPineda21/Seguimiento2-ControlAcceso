using AccessControl.Application.Utilities.Mediator;
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

            // Validations
            services.AddValidatorsFromAssembly(typeof(ApplicationServicesRegistry).Assembly);

            return services;
        }
    }
}
