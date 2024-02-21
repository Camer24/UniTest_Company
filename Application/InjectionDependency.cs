using Application.Interfaces.IServices;
using Application.UseCases;
using Application.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class InjectionDependency
    {
        public static void AddApplicationLayer(this IServiceCollection services) {

            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IPositionService, PositionService>();

            services.AddValidatorsFromAssemblyContaining<CreateEmployeeValidator>();
            services.AddValidatorsFromAssemblyContaining<CreateDepartmentValidator>();
            services.AddValidatorsFromAssemblyContaining<CreatePositionValidator>();
            services.AddValidatorsFromAssemblyContaining<CreateCompanyValidator>();
        }

    }   
}
