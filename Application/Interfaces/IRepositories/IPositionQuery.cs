using Domain.Entities;

namespace Application.Interfaces.IRepositories
{
    public interface IPositionQuery
    {
        Task<IEnumerable<Position>> GetPositionsByCompany(int company);
        Task<Position> GetPosition(int positionId);
        Task<bool> ExistPosition(int positionId);
    }
}

