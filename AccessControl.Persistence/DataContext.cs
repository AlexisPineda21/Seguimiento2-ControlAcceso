using AccessControl.Domain.Entities.AccessPoints;
using AccessControl.Domain.Entities.Areas;
using AccessControl.Domain.Entities.Employees;
using Microsoft.EntityFrameworkCore;

namespace AccessControl.Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Area> Areas { get; set; } = null!;
        public DbSet<AccessPoint> AccessPoints { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);

            base.OnModelCreating(builder);
        }
    }
}
