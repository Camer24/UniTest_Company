using Application.DTO.Request;
using Application.Interfaces.IRepositories;
using FluentValidation;

namespace Application.Validators
{
    public class CreateDepartmentValidator : AbstractValidator<DepartmentRequest>
    {
        private readonly ICompanyQuery _companyQuery;

        public CreateDepartmentValidator(ICompanyQuery companyQuery)
        {

            RuleFor(x => x.Name)
               .NotEmpty().WithMessage("El nombre es requerido.")
               .NotNull().MaximumLength(50).WithMessage("El nombre debe ser menor a than 50 caracteres.");

            this._companyQuery = companyQuery;

            RuleFor(x => x)
                .MustAsync(ExistCompany)
                .WithMessage("La compania no existe.").WithName("Compania");
        }

        private async Task<bool> ExistCompany(DepartmentRequest request, CancellationToken token)
        {
            return await _companyQuery.ExistCompany(request.IdCompany);
        }
    }
}
