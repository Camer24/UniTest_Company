using Domain.Entities;
using Infrastructure.Persistence.Configurations;
using Infrastructure.Persistence.Inserts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class ReportsDbContext : DbContext
    {
        public DbSet<Company> Companys { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=Company;User Id=consulta;Password=consulta;Integrated Security=True;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new PositionConfiguration());

            modelBuilder.ApplyConfiguration(new CompanyInserts());
            modelBuilder.ApplyConfiguration(new DepartmentInserts());
            modelBuilder.ApplyConfiguration(new PositionInserts());
            modelBuilder.ApplyConfiguration(new EmployeeInserts());
        }
    }
}
