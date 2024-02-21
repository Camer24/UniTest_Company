using Application.DTO.Response;
using Domain.Entities;

namespace Application.DTO.Creator
{
    public class PositionCreator
    {
        public PositionResponse Create(Position position)
        {
            return new PositionResponse()
            {
                PositionId = position.Id,
                Description = position.Name,
                Hierarchy = position.Hierarchy,
                MaxAmount = position.MaxAmount,
            };
        }
    }
}
