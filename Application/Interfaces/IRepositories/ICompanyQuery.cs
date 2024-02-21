using Application.DTO.Response;
using Domain.Entities;

namespace Application.Interfaces.IRepositories
{
    public interface ICompanyQuery
    {
        Task<IEnumerable<Company>> GetCompanys();
        Task<Company> GetCompany(int companyId);
        Task<bool> ExistCompany(int companyId);
    }
}
