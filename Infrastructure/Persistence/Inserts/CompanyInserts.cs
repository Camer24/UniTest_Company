using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Inserts
{
    public class CompanyInserts : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasData(new Company()
            {
                CompanyId = 1,
                Cuit = "30-69730872-1",
                Name = "Easy SRL",
                Adress = "Av. Calchaquí 3950",
                Phone = "4229-4000"
            },
            new Company()
            {
                CompanyId = 2,
                Cuit = "33-70892523-9",
                Name = "Remax",
                Adress = "Av. Rivadavia 430",
                Phone = "4253-4987"
            });
        }
    }
}
