

using FluentValidation;

namespace Complexion.DTOs.Dbo.Validators
{
    public class CreateProductValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.CategoryId).NotEmpty().GreaterThan(0);

            RuleFor(x => x.PriceTierId).NotEmpty().GreaterThan(0);

            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);

            RuleFor(x => x.Brand).NotEmpty().MaximumLength(50);

            RuleFor(x => x.ShadeName).NotEmpty().MaximumLength(50);
        }
    }
}
