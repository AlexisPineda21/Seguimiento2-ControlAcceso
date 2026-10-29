using AccessControl.Domain.Entities.AccessPoints;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccessControl.Persistence.Configurations
{
    internal class AccessPointConfig : IEntityTypeConfiguration<AccessPoint>
    {
        public void Configure(EntityTypeBuilder<AccessPoint> builder)
        {
            builder.ToTable("AccessPoints");

            builder.HasKey(accessPoint => accessPoint.Id);

            builder.Property(accessPoint => accessPoint.Name)
                   .HasMaxLength(AccessPoint.NameMaxLength)
                   .IsRequired();

            builder.Property(accessPoint => accessPoint.Description)
                   .HasMaxLength(AccessPoint.DescriptionMaxLength);

            builder.Property(accessPoint => accessPoint.Type)
                   .HasConversion<string>()
                   .HasMaxLength(30)
                   .IsRequired();

            builder.Property(accessPoint => accessPoint.IsEnabled)
                   .IsRequired();

            builder.Property(accessPoint => accessPoint.CreatedAt)
                   .IsRequired();

            builder.HasIndex(accessPoint => accessPoint.Name)
                   .IsUnique();
        }
    }
}
