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
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeCreator _creator;
        private readonly IEmployeeCommand _command;
        private readonly IEmployeeQuery _repository;
        private readonly IValidator<EmployeeRequest> _validator;
        public readonly IPositionService _positionService;

        public EmployeeService(IEmployeeCommand command, IEmployeeQuery repository, IPositionService positionRepository, IValidator<EmployeeRequest> validator)
        {
            _repository = repository;
            _positionService = positionRepository;
            _creator = new EmployeeCreator();
            _command = command;
            _validator = validator;
        }

        public async Task<List<EmployeeResponse>> GetEmployees()
        {
            List<EmployeeResponse> list = new List<EmployeeResponse>();

            IEnumerable<Employee> entities = await _repository.GetEmployees();

            foreach(Employee entity in entities)
            {
                list.Add(_creator.Create(entity));
            }

            return list;
        }

        public async Task<List<EmployeeResponse>> GetSuperiors(int idDep, int position)
        {
            if (idDep < 1)
                throw new BadRequestException("Formato del id del departamento es invalido");
            
            if (position < 1)
                throw new BadRequestException("Formato del id del cargo es invalido");

            Position positionUser = await _positionService.GetPositionEntity(position);
            if (positionUser == null)
                throw new NotFoundException("La posicion no existe.");

            IEnumerable<Employee> entities = await _repository.GetEmployeesByDepartment(idDep);
            entities = entities.Where(x => x.IsApprover && (x.Position.Hierarchy > positionUser.Hierarchy)).ToList();
            
            List<EmployeeResponse> list = new List<EmployeeResponse>();
            foreach (Employee entity in entities)
            {
                list.Add(_creator.Create(entity));
            }

            return list;
        }

        public async Task<EmployeeResponse> GetEmployee(int employeeId)
        {
            if (employeeId < 1)
                throw new BadRequestException("Formato del id del empleado invalido");

            Employee entity = await _repository.GetEmployee(employeeId);

            if(entity == null)
                throw new NotFoundException("El empleado no existe.");

            return _creator.Create(entity);
        }

        public async Task<int> CreateEmployee(EmployeeRequest request)
        {

            ValidationResult validatorResult = await _validator.ValidateAsync(request);

            if(!validatorResult.IsValid)
                throw new BadRequestException("Empleado Invalido", validatorResult);



            Employee employee = new Employee
            {
                Id = request.Id,
                FirstName = request.FirsName,
                LastName = request.LastName,
                DepartamentId = request.DepartmentId,
                SuperiorId = request.SuperiorId,
                PositionId = request.PositionId,
                IsApprover = request.IsApprover
            };

            return await _command.InsertEmployee(employee);
        }

        public async Task<DepartmentResponse> GetEmployeeDepartment(int employeeId)
        {
            if (employeeId < 1)
                throw new BadRequestException("Formato del id del empleado invalido");

            Department? entity = await _repository.GetEmployeeDepartment(employeeId);

            if (entity == null)
                throw new NotFoundException("usuario invalido");

            DepartmentResponse response = new DepartmentResponse()
            {
                DepartmentId = entity.Id,
                Name = entity.Name
            };

            return response;
        }


        public async Task<int> GetNextApprover(int employeeId, int amount)
        {
            if (amount < 0)
                throw new BadRequestException("La cantidad debe ser mayor a cero");

            if (employeeId < 1)
                throw new BadRequestException("usuario invalido");

            Employee employee = await _repository.GetEmployee(employeeId);

            if(employee == null)
                throw new NotFoundException("usuario invalido");

            if(employee.Position.MaxAmount >= amount)
                return 0;

            return (int)((employee.SuperiorId == null) ? 0 : employee.SuperiorId);
        }

        public async Task<int> GetApprover(int id)
        {
            if(id < 1)
                throw new BadRequestException("usuario invalido");

            Employee entity = await _repository.GetEmployee(id);

            if(entity == null)
                throw new NotFoundException("usuario invalido");

            return (int)((entity.SuperiorId == null) ? 0 : entity.SuperiorId);
        }

        public async Task<int> DeleteEmployee(int id)
        {
            if (id < 1)
                throw new BadRequestException("usuario invalido");

            Employee entity = await _repository.GetEmployee(id);

            if(entity == null)
                throw new NotFoundException("empleado invalido");

            return await _command.DeleteEmployee(entity);
        }

        public async Task<int> EnableHistoryFlag(int id)
        {
            if (id < 1)
                throw new EmployeeIdFormatException();

            Employee entity = await _repository.GetEmployee(id);

            if(entity == null)
                throw new NotFoundException("empleado invalido");

            return await _command.AcceptHistoryFlag(entity);
        }

        public async Task<int> DisableHistoryFlag(int id)
        {
            if (id < 1)
                throw new EmployeeIdFormatException();

            Employee entity = await _repository.GetEmployee(id);

            if(entity == null)
                throw new NotFoundException("empleado invalido");

            return await _command.DissmisHistoryFlag(entity);
        }

        public async Task<int> EnableApprovalsFlagFlag(int id)
        {
            if (id < 1)
                throw new EmployeeIdFormatException();

            Employee entity = await _repository.GetEmployee(id);

            if(entity == null)
                throw new NotFoundException("empleado invalido");

            return await _command.AcceptApprovalsFlagFlag(entity);
        }

        public async Task<int> DisableApprovalsFlag(int id)
        {
            if (id < 1)
                throw new EmployeeIdFormatException();

            Employee entity = await _repository.GetEmployee(id);

            if(entity == null)
                throw new NotFoundException("empleado invalido");

            return await _command.DissmisApprovalsFlag(entity);
        }

    }
}