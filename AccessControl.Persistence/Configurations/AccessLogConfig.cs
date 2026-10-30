using AccessControl.Domain.Entities.AccessLogs;
using AccessControl.Domain.Entities.Credentials;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccessControl.Persistence.Configurations
{
    internal class AccessLogConfig : IEntityTypeConfiguration<AccessLog>
    {
        public void Configure(EntityTypeBuilder<AccessLog> builder)
        {
            builder.ToTable("AccessLogs");

            builder.HasKey(accessLog => accessLog.Id);

            builder.Property(accessLog => accessLog.CredentialCode)
                   .HasMaxLength(Credential.CodeMaxLength)
                   .IsRequired();

            builder.Property(accessLog => accessLog.MovementType)
                   .HasConversion<string>()
                   .HasMaxLength(10)
                   .IsRequired();

            builder.Property(accessLog => accessLog.Result)
                   .HasConversion<string>()
                   .HasMaxLength(15)
                   .IsRequired();

            builder.Property(accessLog => accessLog.RejectionReason)
                   .HasMaxLength(AccessLog.ReasonMaxLength);

            builder.Property(accessLog => accessLog.OccurredAt)
                   .IsRequired();

            builder.HasOne(accessLog => accessLog.Credential)
                   .WithMany()
                   .HasForeignKey(accessLog => accessLog.CredentialId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(accessLog => accessLog.Employee)
                   .WithMany()
                   .HasForeignKey(accessLog => accessLog.EmployeeId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(accessLog => accessLog.AccessPoint)
                   .WithMany()
                   .HasForeignKey(accessLog => accessLog.AccessPointId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(accessLog => accessLog.OccurredAt);
            builder.HasIndex(accessLog => new { accessLog.EmployeeId, accessLog.AccessPointId, accessLog.OccurredAt });
        }
    }
}
