using Application.DTO.Response;
using Domain.Entities;

namespace Application.Interfaces.IRepositories
{
    public interface IDepartmentCommand
    {
        Task<int> DeleteDepartment(Department entity);
        Task<int> InsertDepartment(Department department);
    }
}
