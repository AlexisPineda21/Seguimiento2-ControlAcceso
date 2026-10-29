using AccessControl.Domain.Entities.Areas;
using AccessControl.Domain.Entities.Employees;
using AccessControl.Domain.Entities.Employees.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccessControl.Persistence.Configurations
{
    internal class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");

            builder.HasKey(employee => employee.Id);

            builder.OwnsOne(employee => employee.DocumentNumber, documentNumber =>
            {
                documentNumber.Property(document => document.Value)
                              .HasColumnName("DocumentNumber")
                              .HasMaxLength(DocumentNumber.MaxLength)
                              .IsRequired();

                documentNumber.HasIndex(document => document.Value)
                              .IsUnique();
            });

            builder.OwnsOne(employee => employee.Email, email =>
            {
                email.Property(value => value.Value)
                     .HasColumnName("Email")
                     .HasMaxLength(Email.MaxLength)
                     .IsRequired();

                email.HasIndex(value => value.Value)
                     .IsUnique();
            });

            builder.Property(employee => employee.FirstName)
                   .HasMaxLength(Employee.NameMaxLength)
                   .IsRequired();

            builder.Property(employee => employee.LastName)
                   .HasMaxLength(Employee.NameMaxLength)
                   .IsRequired();

            builder.Property(employee => employee.Position)
                   .HasMaxLength(Employee.PositionMaxLength)
                   .IsRequired();

            builder.Property(employee => employee.Status)
                   .HasConversion<string>()
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(employee => employee.CreatedAt)
                   .IsRequired();

            builder.HasOne(employee => employee.Area)
                   .WithMany()
                   .HasForeignKey(employee => employee.AreaId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Ignore(employee => employee.IsActive);
            builder.Ignore(employee => employee.FullName);
        }
    }
}
