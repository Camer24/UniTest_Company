using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(e => e.CompanyId);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Adress)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Cuit)
                .IsRequired()
                .HasMaxLength(13);

            builder.Property(e => e.Phone)
                .IsRequired()
            .HasMaxLength(13);

        }
    }
}
