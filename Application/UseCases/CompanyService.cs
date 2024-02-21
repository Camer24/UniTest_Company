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
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyQuery _query;
        private readonly CompanyCreator _creator;
        private readonly ICompanyCommand _command;
        private readonly IValidator<CompanyRequest> _validator;

        public CompanyService(ICompanyQuery repository, ICompanyCommand command, IValidator<CompanyRequest> validator)
        {
            _query = repository;
            _creator = new CompanyCreator();
            _command = command;
            _validator = validator;
        }

        public async Task<List<CompanyResponse>> GetCompanys()
        {
            List<CompanyResponse> list = new List<CompanyResponse>();
            IEnumerable<Company> entities = await _query.GetCompanys();

            foreach (Company entity in entities)
            {
                list.Add(_creator.Create(entity));
            }

            return list;
        }
        
        public async Task<CompanyResponse> GetCompany(int companyId)
        {
            Company entity = await _query.GetCompany(companyId);

            if(entity == null)
                throw new NotFoundException("el id no corresponde a una compania.");

            return _creator.Create(entity);           
        }

        public async Task<int> CreateCompany(CompanyRequest request)
        {

            ValidationResult validatorResult = await _validator.ValidateAsync(request);

            if(!validatorResult.IsValid)
                throw new BadRequestException("Compania Invalida", validatorResult);

            Company company = new Company
            {
                Cuit = request.Cuit,
                Name = request.Name,
                Adress = request.Adress,
                Phone = request.Phone
            };

            return await _command.InsertCompany(company);
        }
    }
}
