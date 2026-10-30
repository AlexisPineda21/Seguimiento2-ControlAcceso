using AccessControl.Domain.Entities.Credentials;
using AccessControl.Domain.Entities.Employees;
using Microsoft.EntityFrameworkCore;

namespace AccessControl.Persistence.Seeds
{
    public class CredentialSeeder : IDataSeeder
    {
        private readonly DataContext _context;

        public CredentialSeeder(DataContext context)
        {
            _context = context;
        }

        public int Order => 4;

        public async Task SeedAsync(CancellationToken cancellationToken = default)
        {
            if (await _context.Credentials.AnyAsync(cancellationToken))
            {
                return;
            }

            List<Employee> employees = await _context.Employees.ToListAsync(cancellationToken);

            Guid lauraId = employees.First(employee => employee.DocumentNumber.Value == "1036625478").Id;
            Guid andresId = employees.First(employee => employee.DocumentNumber.Value == "71589632").Id;
            Guid valentinaId = employees.First(employee => employee.DocumentNumber.Value == "1152698745").Id;
            Guid carlosId = employees.First(employee => employee.DocumentNumber.Value == "1017456321").Id;

            Credential lostCredential = new("CRD-1003", valentinaId);
            lostCredential.Block("Reporte de pérdida del carné");

            List<Credential> credentials =
            [
                new Credential("CRD-1001", lauraId),
                new Credential("CRD-1002", andresId),
                lostCredential,
                new Credential("CRD-1004", carlosId)
            ];

            await _context.Credentials.AddRangeAsync(credentials, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
