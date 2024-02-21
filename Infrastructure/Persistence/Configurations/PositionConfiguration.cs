using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal class PositionConfiguration : IEntityTypeConfiguration<Position>
    {
        public void Configure(EntityTypeBuilder<Position> builder)
        {

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Hierarchy)
                .IsRequired();

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.MaxAmount)
                    .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(e => e.IdCompany)
                    .IsRequired();

            builder.HasOne<Company>(u => u.Company)
                  .WithMany(r => r.Positions)
                  .HasForeignKey(u => u.IdCompany);

        }
    }
}
