using Microsoft.Extensions.DependencyInjection;

namespace AccessControl.Persistence.Seeds
{
    public static class DataBaseSeeder
    {
        public static async Task SeedAsync(IServiceProvider services, CancellationToken cancellationToken = default)
        {
            using IServiceScope scope = services.CreateScope();

            IEnumerable<IDataSeeder> seeders = scope.ServiceProvider.GetServices<IDataSeeder>()
                                                                    .OrderBy(seeder => seeder.Order);

            foreach (IDataSeeder seeder in seeders)
            {
                await seeder.SeedAsync(cancellationToken);
            }
        }
    }
}
