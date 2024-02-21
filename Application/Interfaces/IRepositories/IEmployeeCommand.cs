using Domain.Entities;

namespace Application.Interfaces.IRepositories
{
    public interface IEmployeeCommand
    {
        Task<int> AcceptApprovalsFlagFlag(Employee entity);
        Task<int> AcceptHistoryFlag(Employee entity);
        Task<int> DeleteEmployee(Employee employee);
        Task<int> DissmisApprovalsFlag(Employee entity);
        Task<int> DissmisHistoryFlag(Employee entity);
        Task<int> InsertEmployee(Employee employee);
    }
}
