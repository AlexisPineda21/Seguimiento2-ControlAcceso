using AccessControl.Domain.Entities.Areas;
using Microsoft.EntityFrameworkCore;

namespace AccessControl.Persistence.Seeds
{
    public class AreaSeeder : IDataSeeder
    {
        private readonly DataContext _context;

        public AreaSeeder(DataContext context)
        {
            _context = context;
        }

        public int Order => 1;

        public async Task SeedAsync(CancellationToken cancellationToken = default)
        {
            if (await _context.Areas.AnyAsync(cancellationToken))
            {
                return;
            }

            List<Area> areas =
            [
                new Area("Administración", "Gerencia, contabilidad y gestión administrativa"),
                new Area("Operaciones logísticas", "Recepción, despacho y distribución de mercancía"),
                new Area("Almacenamiento", "Personal de bodegas e inventarios"),
                new Area("Seguridad", "Vigilancia y control de ingresos"),
                new Area("Talento humano", "Selección, contratación y bienestar del personal"),
                new Area("Tecnología", "Soporte técnico e infraestructura")
            ];

            await _context.Areas.AddRangeAsync(areas, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
