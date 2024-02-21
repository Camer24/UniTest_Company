using Application.Interfaces.IRepositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Querys
{
    public class PositionQuery : IPositionQuery
    {
        private readonly ReportsDbContext _dbContext;

        public PositionQuery(ReportsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> ExistPosition(int positionId)
        {
            return await _dbContext.Positions.AnyAsync(p => p.Id == positionId);
        }

        public async Task<Position> GetPosition(int positionId)
        {
            return await _dbContext.Positions.FirstOrDefaultAsync(x => x.Id == positionId);
        }

        public async Task<IEnumerable<Position>> GetPositionsByCompany(int company)
        {
            return await _dbContext.Positions.Where(x => x.IdCompany == company).ToListAsync();
        }
    }
}

