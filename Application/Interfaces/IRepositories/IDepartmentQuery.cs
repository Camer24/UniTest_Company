using Domain.Entities;

namespace Application.Interfaces.IRepositories
{
    public interface IDepartmentQuery
    {
        Task<IEnumerable<Department>> GetDepartmentsByCompany(int company);
        Task<Department> GetDepartment(int departmentId);
        Task<bool> ExistDepartment(int departmentId);
    }
}
