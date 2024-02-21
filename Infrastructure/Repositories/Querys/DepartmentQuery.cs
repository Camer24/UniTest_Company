using Application.Interfaces.IRepositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Querys
{
    public class DepartmentQuery : IDepartmentQuery
    {
        private readonly ReportsDbContext _dbContext;

        public DepartmentQuery(ReportsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> ExistDepartment(int departmentId)
        {
            return await _dbContext.Departments.AnyAsync(d => d.Id == departmentId);
        }

        public async Task<Department> GetDepartment(int departmentId)
        {
            return await _dbContext.Departments.FirstOrDefaultAsync(d => d.Id == departmentId);
        }

        public async Task<IEnumerable<Department>> GetDepartmentsByCompany(int idCompany)
        {
            return await _dbContext.Departments.Where(d => d.IdCompany == idCompany).ToListAsync();
        }
    }
}
