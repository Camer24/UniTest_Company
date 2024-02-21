using Application.Interfaces.IRepositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Querys
{
    public class CompanyQuery : ICompanyQuery
    {
        private readonly ReportsDbContext _dbContext;

        public CompanyQuery(ReportsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> ExistCompany(int companyId)
        {
            return await _dbContext.Companys.AnyAsync(x => x.CompanyId == companyId);
        }

        public async Task<Company> GetCompany(int companyId)
        {
            return await _dbContext.Companys.FirstOrDefaultAsync(x => x.CompanyId == companyId);
        }

        public async Task<IEnumerable<Company>> GetCompanys()
        {
            return await _dbContext.Companys.ToListAsync();
        }
    }
}