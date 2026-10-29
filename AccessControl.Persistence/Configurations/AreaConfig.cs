using AccessControl.Domain.Entities.Areas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccessControl.Persistence.Configurations
{
    internal class AreaConfig : IEntityTypeConfiguration<Area>
    {
        public void Configure(EntityTypeBuilder<Area> builder)
        {
            builder.ToTable("Areas");

            builder.HasKey(area => area.Id);

            builder.Property(area => area.Name)
                   .HasMaxLength(Area.NameMaxLength)
                   .IsRequired();

            builder.Property(area => area.Description)
                   .HasMaxLength(Area.DescriptionMaxLength);

            builder.HasIndex(area => area.Name)
                   .IsUnique();
        }
    }
}
