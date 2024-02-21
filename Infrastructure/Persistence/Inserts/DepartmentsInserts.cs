using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Inserts
{
    public class DepartmentInserts : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasData(new Department()
            {
                Id = 1,
                Name = "Recursos Humanos",
                IdCompany = 1
            },
            new Department()
            {
                Id = 2,
                Name = "Marketing",
                IdCompany = 1
            },
            new Department()
            {
                Id = 3,
                Name = "Comercial",
                IdCompany = 1
            },
            new Department()
            {
                Id = 4,
                Name = "Control de Gestión",
                IdCompany = 2
            },
            new Department()
            {
                Id = 5,
                Name = "Logística y Operaciones",
                IdCompany = 2
            });
        }
    }
}
