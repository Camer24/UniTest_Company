using Application.DTO.Request;
using Application.Interfaces.IRepositories;
using FluentValidation;

namespace Application.Validators
{
    public class CreatePositionValidator : AbstractValidator<PositionRequest>
    {
        private readonly ICompanyQuery _companyQuery;

        public CreatePositionValidator(ICompanyQuery companyQuery)
        {
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("La descripcion es requerida.")
                .NotNull().MaximumLength(50).WithMessage("El descripcion debe ser menor a than 50 caracteres.");


            RuleFor(x => x.MaxAmount)
                .NotNull().WithMessage("El maximo es requerida.")
                .GreaterThan(0).WithMessage("El maximo debe ser mayor a 0");

            RuleFor(x => x.Hierarchy)
               .NotNull().WithMessage("La jerarquia es requerida.")
               .GreaterThan(0).WithMessage("El maximo debe ser mayor a 0");

            this._companyQuery = companyQuery;

            RuleFor(x => x.Hierarchy)
              .NotNull().WithMessage("La jerarquia es requerida.")
              .GreaterThan(0).WithMessage("El maximo debe ser mayor a 0");

            RuleFor(x => x)
              .MustAsync(ExistCompany)
              .WithMessage("La compania no existe.").WithName("Compania");

        }

        private async Task<bool> ExistCompany(PositionRequest request, CancellationToken token)
        {
            return await _companyQuery.ExistCompany(request.CompanyId);
        }
    }
}
