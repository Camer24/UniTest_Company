using Application.Interfaces.IRepositories;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories.Commands
{
    public class PositionCommand : IPositionCommand
    {
        private readonly ReportsDbContext _dbContext;

        public PositionCommand(ReportsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> DeletePosition(Position entity)
        {
            _dbContext.Positions.Remove(entity);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> InsertPosition(Position position)
        {
            _dbContext.Add(position);

            return await _dbContext.SaveChangesAsync();
        }
    }
}
