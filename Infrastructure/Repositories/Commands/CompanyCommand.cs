using Application.Interfaces.IRepositories;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories.Commands
{
    public class CompanyCommand : ICompanyCommand
    {
        private readonly ReportsDbContext _dbContext;
        public CompanyCommand(ReportsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> InsertCompany(Company company)
        {
            _dbContext.Add(company);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
