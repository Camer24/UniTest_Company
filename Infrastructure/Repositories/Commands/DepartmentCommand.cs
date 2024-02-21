using Application.Interfaces.IRepositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Commands
{
    public class DepartmentCommand : IDepartmentCommand
    {
        private readonly ReportsDbContext _dbContext;

        public DepartmentCommand(ReportsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> DeleteDepartment(Department entity)
        {
            _dbContext.Departments.Remove(entity);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> InsertDepartment(Department department)
        {
            _dbContext.Add(department);
            return await _dbContext.SaveChangesAsync();
        }

    }
}