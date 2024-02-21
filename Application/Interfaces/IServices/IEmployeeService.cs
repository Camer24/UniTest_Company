using Application.DTO.Request;
using Application.DTO.Response;

namespace Application.Interfaces.IServices
{
    public interface IEmployeeService
    {
        Task<List<EmployeeResponse>> GetEmployees();
        Task<List<EmployeeResponse>> GetSuperiors(int idDep, int position);

        Task<EmployeeResponse> GetEmployee(int employeeId);
        Task<int> CreateEmployee(EmployeeRequest request);
        Task<int> GetApprover(int id);
        public Task<int> GetNextApprover(int employeeId, int amount);
        Task<DepartmentResponse> GetEmployeeDepartment(int idUser);
        Task<int> DeleteEmployee(int id);
        Task<int> EnableHistoryFlag(int id);
        Task<int> DisableHistoryFlag(int id);
        Task<int> EnableApprovalsFlagFlag(int id);
        Task<int> DisableApprovalsFlag(int id);
    }
}