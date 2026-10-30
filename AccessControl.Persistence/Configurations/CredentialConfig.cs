using AccessControl.Domain.Entities.Credentials;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccessControl.Persistence.Configurations
{
    internal class CredentialConfig : IEntityTypeConfiguration<Credential>
    {
        public void Configure(EntityTypeBuilder<Credential> builder)
        {
            builder.ToTable("Credentials");

            builder.HasKey(credential => credential.Id);

            builder.Property(credential => credential.Code)
                   .HasMaxLength(Credential.CodeMaxLength)
                   .IsRequired();

            builder.HasIndex(credential => credential.Code)
                   .IsUnique();

            builder.Property(credential => credential.Status)
                   .HasConversion<string>()
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(credential => credential.StatusReason)
                   .HasMaxLength(Credential.ReasonMaxLength);

            builder.Property(credential => credential.IssuedAt)
                   .IsRequired();

            builder.HasOne(credential => credential.Employee)
                   .WithMany()
                   .HasForeignKey(credential => credential.EmployeeId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Ignore(credential => credential.CanBeUsed);
        }
    }
}
