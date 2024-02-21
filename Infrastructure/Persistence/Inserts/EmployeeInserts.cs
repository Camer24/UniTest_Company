using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Persistence.Inserts
{
    public class EmployeeInserts : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasData(
                    new Employee()
                    {
                        Id = 1,
                        FirstName = "diego",
                        LastName = "rodriguez",
                        DepartamentId = 1,
                        PositionId = 1,
                        HistoryFlag = false,
                        ApprovalsFlag = false,
                    },
                    new Employee()
                    {
                        Id = 2,
                        FirstName = "jose",
                        LastName = "martinez",
                        DepartamentId = 1,
                        PositionId = 2,
                        SuperiorId = 1,
                        HistoryFlag = false,
                        ApprovalsFlag = false,
                    },
                    new Employee()
                    {
                        Id = 3,
                        FirstName = "Miguel Ángel",
                        LastName = "Merentiel",
                        DepartamentId = 1,
                        PositionId = 1,
                        SuperiorId = 2,
                        HistoryFlag = false,
                        ApprovalsFlag = false,
                    });
        }
    }
}

