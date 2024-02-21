using Application.DTO.Request;
using Application.DTO.Response;
using Domain.Entities;

namespace Application.Interfaces.IServices
{
    public interface IPositionService
    {
        Task<List<PositionResponse>> GetPositionsByCompany(int company);
        Task<PositionResponse> GetPosition(int positionId);
        Task<Position> GetPositionEntity(int positionId);
        Task<int> CreatePosition(PositionRequest request);
        Task<int> DeletePosition(int id);
    }
}
