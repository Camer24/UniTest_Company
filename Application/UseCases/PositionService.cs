using Application.DTO.Creator;
using Application.DTO.Request;
using Application.DTO.Response;
using Application.Exceptions;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Domain.Entities;
using FluentValidation;
using FluentValidation.Results;


namespace Application.UseCases
{
    public class PositionService : IPositionService
    {
        private IPositionQuery _repository;
        private PositionCreator _creator;
        private IPositionCommand _command;
        private readonly IValidator<PositionRequest> _validator;

        public PositionService(IPositionQuery repository, IPositionCommand command, IValidator<PositionRequest> validator)
        {
            _repository = repository;
            _creator = new PositionCreator();
            _command = command;
            _validator = validator;
        }

        public async Task<List<PositionResponse>> GetPositionsByCompany(int company)
        {
            if (company < 1)
                throw new BadRequestException("Formato del id de la compania es invalido");

            IEnumerable<Position> entities = await _repository.GetPositionsByCompany(company);

            List<PositionResponse> list = entities.Select(e => new PositionResponse()
            {
                PositionId = e.Id,
                Hierarchy = e.Hierarchy,
                Description = e.Name,
                MaxAmount = e.MaxAmount
            }).ToList();

            return list;
        }

        public async Task<Position> GetPositionEntity(int positionId)
        {
            if (positionId < 1)
                throw new BadRequestException("Formato del id de la posicion es invalido");
            Position entity = await _repository.GetPosition(positionId);

            if (entity == null)
                throw new NotFoundException("La posicion no existe.");

            return entity;
        }

        public async Task<PositionResponse> GetPosition(int positionId)
        {
            Position entity = await GetPositionEntity(positionId);

            return _creator.Create(entity);
        }

        public async Task<int> CreatePosition(PositionRequest request)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(request);

            if(!validationResult.IsValid)
                throw new BadRequestException("Posicion Invalida", validationResult);

            Position position = new Position
            {
                Name = request.Description,
                Hierarchy = request.Hierarchy,
                MaxAmount = request.MaxAmount,
                IdCompany = request.CompanyId
            };

            return await _command.InsertPosition(position);
        }

        public async Task<int> DeletePosition(int id)
        {
            if (id < 1)
                throw new BadRequestException("Formato del id de la posicion es invalido");
            Position entity = await _repository.GetPosition(id);

            if(entity == null)
                throw new NotFoundException("La posicion no existe.");

            return await _command.DeletePosition(entity);
        }
    }
}