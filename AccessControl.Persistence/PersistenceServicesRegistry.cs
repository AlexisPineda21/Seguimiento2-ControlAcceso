using AccessControl.Application.Contracts.Persistence;
using AccessControl.Application.Contracts.Repositories;
using AccessControl.Persistence.Repositories;
using AccessControl.Persistence.Seeds;
using AccessControl.Persistence.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AccessControl.Persistence
{
    public static class PersistenceServicesRegistry
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IUnitOfWork, EfCoreUnitOfWork>();

            // Repositories
            services.AddScoped<IAreasRepository, AreasRepository>();
            services.AddScoped<IAccessPointsRepository, AccessPointsRepository>();
            services.AddScoped<IEmployeesRepository, EmployeesRepository>();

            // Seeders
            services.AddScoped<IDataSeeder, AreaSeeder>();
            services.AddScoped<IDataSeeder, AccessPointSeeder>();
            services.AddScoped<IDataSeeder, EmployeeSeeder>();

            return services;
        }
    }
}
