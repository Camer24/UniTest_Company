using Application.DTO.Request;
using Application.DTO.Response;
using Domain.Entities;

namespace Application.Interfaces.IServices
{
    public interface ICompanyService
    {
        Task<List<CompanyResponse>> GetCompanys();
        Task<CompanyResponse> GetCompany(int companyId);
        public Task<int> CreateCompany(CompanyRequest request);
    }
}
