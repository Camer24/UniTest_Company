using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Inserts
{
    public class PositionInserts : IEntityTypeConfiguration<Position>
    {
        public void Configure(EntityTypeBuilder<Position> builder)
        {
            builder.HasData(
                   new Position()
                   {
                       Id = 1,
                       Name = "Director",
                       Hierarchy = 10,
                       MaxAmount = 50000,
                       IdCompany = 1,
                   },
                   new Position()
                   {
                       Id = 2,
                       Name = "Lider",
                       Hierarchy = 10,
                       MaxAmount = 50000,
                       IdCompany = 1,
                   },
                   new Position()
                   {
                       Id = 3,
                       Name = "Empleado",
                       Hierarchy = 10,
                       MaxAmount = 500,
                       IdCompany = 1,
                   });
        }
    }
}
