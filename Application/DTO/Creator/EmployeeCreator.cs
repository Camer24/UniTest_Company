using Application.DTO.Response;
using Domain.Entities;

namespace Application.DTO.Creator
{
    public class EmployeeCreator
    {
        public EmployeeResponse Create(Employee employee)
        {
            return new EmployeeResponse()
            {
                Id = employee.Id,
                FirsName = employee.FirstName,
                LastName = employee.LastName,
                DepartmentId = employee.DepartamentId,
                SuperiorId = employee.SuperiorId,
                PositionId = employee.PositionId,
                CompanyId = employee.Departament.IdCompany,
                IsApprover = employee.IsApprover
            };
        }
    }
}