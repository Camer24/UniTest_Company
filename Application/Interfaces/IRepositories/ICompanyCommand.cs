using Domain.Entities;

namespace Application.Interfaces.IRepositories
{
    public interface ICompanyCommand
    {
        Task<int> InsertCompany(Company company);
    }
}
