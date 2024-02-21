using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                    .ValueGeneratedNever();

            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.HistoryFlag)
                .IsRequired();

            builder.Property(e => e.ApprovalsFlag)
                .IsRequired();

            builder.Property(e => e.IsApprover)
                .IsRequired();

            builder.Property(e => e.SuperiorId)
                    .IsRequired(false);

            builder.HasOne<Employee>(u => u.Superior)
                    .WithMany(u => u.Subordinates)
                    .HasForeignKey(u => u.SuperiorId)
                    .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne<Position>(u => u.Position)
                    .WithMany(u => u.Employees)
                    .HasForeignKey(u => u.PositionId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Department>(u => u.Departament)
                    .WithMany(u => u.Employees)
                    .HasForeignKey(u => u.DepartamentId)
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

