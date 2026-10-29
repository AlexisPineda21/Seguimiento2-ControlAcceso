using AccessControl.Domain.Entities.Areas;
using AccessControl.Domain.Entities.Employees;
using AccessControl.Domain.Entities.Employees.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace AccessControl.Persistence.Seeds
{
    public class EmployeeSeeder : IDataSeeder
    {
        private readonly DataContext _context;

        public EmployeeSeeder(DataContext context)
        {
            _context = context;
        }

        public int Order => 3;

        public async Task SeedAsync(CancellationToken cancellationToken = default)
        {
            if (await _context.Employees.AnyAsync(cancellationToken))
            {
                return;
            }

            List<Area> areas = await _context.Areas.ToListAsync(cancellationToken);

            Guid storageAreaId = areas.First(area => area.Name == "Almacenamiento").Id;
            Guid securityAreaId = areas.First(area => area.Name == "Seguridad").Id;
            Guid technologyAreaId = areas.First(area => area.Name == "Tecnología").Id;

            Employee formerEmployee = new(new DocumentNumber("1017456321"), "Carlos", "Restrepo",
                                          new Email("carlos.restrepo@empresa.com"), "Auxiliar de bodega", storageAreaId);
            formerEmployee.Deactivate();

            List<Employee> employees =
            [
                new Employee(new DocumentNumber("1036625478"), "Laura", "Gómez",
                             new Email("laura.gomez@empresa.com"), "Coordinadora de inventarios", storageAreaId),
                new Employee(new DocumentNumber("71589632"), "Andrés", "Zapata",
                             new Email("andres.zapata@empresa.com"), "Supervisor de seguridad", securityAreaId),
                new Employee(new DocumentNumber("1152698745"), "Valentina", "Ríos",
                             new Email("valentina.rios@empresa.com"), "Analista de soporte", technologyAreaId),
                formerEmployee
            ];

            await _context.Employees.AddRangeAsync(employees, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
