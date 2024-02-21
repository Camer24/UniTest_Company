using Application.DTO.Response;
using Domain.Entities;

namespace Application.Interfaces.IRepositories
{
    public interface IPositionCommand
    {
        Task<int> DeletePosition(Position entity);
        Task<int> InsertPosition(Position position);
    }
}
