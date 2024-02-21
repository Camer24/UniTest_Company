using Application.Interfaces.IRepositories;
using Infrastructure.Persistence;
using Infrastructure.Repositories.Commands;
using Infrastructure.Repositories.Querys;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InjectionDependency
    {
        public static void AddInfraestructureLayer(this IServiceCollection services)
        {
            services.AddScoped<ReportsDbContext>();
            services.AddScoped<IDepartmentQuery, DepartmentQuery>();
            services.AddScoped<ICompanyQuery, CompanyQuery>();
            services.AddScoped<IDepartmentCommand, DepartmentCommand>();
            services.AddScoped<ICompanyCommand, CompanyCommand>();
            services.AddScoped<IEmployeeQuery, EmployeeQuery>();
            services.AddScoped<IPositionQuery, PositionQuery>();
            services.AddScoped<IEmployeeCommand, EmployeeCommand>();
            services.AddScoped<IPositionCommand, PositionCommand>();
        }
    }
}
