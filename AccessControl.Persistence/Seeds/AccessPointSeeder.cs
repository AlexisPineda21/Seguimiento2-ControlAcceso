using AccessControl.Domain.Entities.AccessPoints;
using Microsoft.EntityFrameworkCore;

namespace AccessControl.Persistence.Seeds
{
    public class AccessPointSeeder : IDataSeeder
    {
        private readonly DataContext _context;

        public AccessPointSeeder(DataContext context)
        {
            _context = context;
        }

        public int Order => 2;

        public async Task SeedAsync(CancellationToken cancellationToken = default)
        {
            if (await _context.AccessPoints.AnyAsync(cancellationToken))
            {
                return;
            }

            AccessPoint warehouseUnderMaintenance = new("Bodega 2", AccessPointType.Warehouse, "Bodega en mantenimiento");
            warehouseUnderMaintenance.Disable();

            List<AccessPoint> accessPoints =
            [
                new AccessPoint("Recepción principal", AccessPointType.Reception, "Ingreso principal de la sede"),
                new AccessPoint("Parqueadero", AccessPointType.Parking, "Ingreso vehicular y de motos"),
                new AccessPoint("Bodega 1", AccessPointType.Warehouse, "Bodega de almacenamiento general"),
                new AccessPoint("Cuarto de servidores", AccessPointType.RestrictedArea, "Área restringida de tecnología"),
                warehouseUnderMaintenance
            ];

            await _context.AccessPoints.AddRangeAsync(accessPoints, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
