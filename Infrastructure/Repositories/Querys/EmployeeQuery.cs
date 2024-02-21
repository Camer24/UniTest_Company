using Application.Interfaces.IRepositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Querys
{
    public class EmployeeQuery : IEmployeeQuery
    {
        private readonly ReportsDbContext _dbContext;

        public EmployeeQuery(ReportsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Department> GetEmployeeDepartment(int IdUser)
        {
            return await _dbContext.Employees
                            .Where(e => e.Id == IdUser)
                            .Select(e => e.Departament)
                            .FirstOrDefaultAsync();
        }

        public async Task<Employee> GetEmployeeShallow(int? employeeId)
        {
            return await _dbContext.Employees
                                   .FirstOrDefaultAsync(x => x.Id == employeeId);
        }

        public async Task<Employee> GetEmployee(int? employeeId)
        {
            return await _dbContext.Employees
                            .Include(x => x.Position)
                            .Include(x => x.Departament)
                            .FirstOrDefaultAsync(x => x.Id == employeeId);
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _dbContext.Employees.Include(x => x.Departament).ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByDepartment(int dep)
        {
            return await _dbContext.Employees.Where(x => x.DepartamentId == dep)
                .Include(x => x.Departament).Include(x => x.Position).ToListAsync();
        }
    }
}