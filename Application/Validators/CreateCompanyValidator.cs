using Application.DTO.Request;
using FluentValidation;

namespace Application.Validators
{
    public class CreateCompanyValidator : AbstractValidator<CompanyRequest>
    {
        public CreateCompanyValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre es requerido.")
                .NotNull().MaximumLength(50).WithMessage("El nombre debe ser menor a que 50 caracteres.");


            RuleFor(x => x.Cuit)
                .NotEmpty().WithMessage("El cuit es requerido.")
                .NotNull().MaximumLength(13).WithMessage("El cuit debe ser menor a que 13 caracteres.");

            RuleFor(x => x.Adress)
                .NotEmpty().WithMessage("El cuit es requerido.")
                .NotNull().MaximumLength(100).WithMessage("La dirrecion debe ser menor a que 50 caracteres.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("El telefono es requerido.")
                .NotNull().MaximumLength(13).WithMessage("El telefono debe ser menor a que 13 caracteres.");
        }
    }
}
