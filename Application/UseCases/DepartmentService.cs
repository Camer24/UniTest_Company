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
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentQuery _query;
        private readonly IDepartmentCommand _command;
        private readonly DepartmentCreator _creator;
        private readonly IValidator<DepartmentRequest> _validator;


        public DepartmentService(IDepartmentQuery repository, IDepartmentCommand command, IValidator<DepartmentRequest> validator)
        {
            _query = repository;
            _creator = new DepartmentCreator();
            _command = command;
            _validator = validator;
        }

        public async Task<IList<DepartmentResponse>> GetDepartmentsByCompany(int idCompany)
        {
            IList<DepartmentResponse> list = new List<DepartmentResponse>();
            if (idCompany == null || idCompany < 0)
                throw new BadRequestException("Id invalido");
            IEnumerable<Department> entities = await _query.GetDepartmentsByCompany(idCompany);

            foreach(Department entity in entities)
            {
                list.Add(_creator.Create(entity));
            }
            return list;
        }

        public async Task<DepartmentResponse> GetDepartment(int departmentId)
        {
            if (departmentId == null || departmentId < 0)
                throw new BadRequestException("Id invalido");

            Department entity = await _query.GetDepartment(departmentId);

            if(entity == null)
                throw new NotFoundException("El departamento no existe.");

            return _creator.Create(entity);
        }

        public async Task<int> CreateDepartment(DepartmentRequest request)
        {
            ValidationResult validatorResult = await _validator.ValidateAsync(request);

            if(!validatorResult.IsValid)
                throw new BadRequestException("Departamento Invalido", validatorResult);

            Department department = new Department
            {
                Name = request.Name,
                IdCompany = request.IdCompany
            };

            return await _command.InsertDepartment(department);
        }

        public async Task<int> DeleteDepartment(int id)
        {
            if (id == null || id < 0)
                throw new BadRequestException("Id invalido");

            Department entity = await _query.GetDepartment(id);

            if(entity == null)
                throw new NotFoundException("El departamento no existe.");

            return await _command.DeleteDepartment(entity);
        }
    }
}