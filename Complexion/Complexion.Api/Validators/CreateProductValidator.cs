using Complexion.DTOs.Dbo;
using FluentValidation;

namespace Complexion.Api.Validators
{
    public class CreateProductValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductValidator()
        {
            RuleFor(product => product.CategoryId)
                .GreaterThan(0)
                .WithMessage("CategoryId is required.");

            RuleFor(product => product.PriceTierId)
                .GreaterThan(0)
                .WithMessage("PriceTierId is required.");

            RuleFor(product => product.Name)
                .NotEmpty()
                .WithMessage("Product name is required.")
                .MaximumLength(50)
                .WithMessage("Product name must be 50 characters or fewer.");

            RuleFor(product => product.Brand)
                .NotEmpty()
                .WithMessage("Brand is required.")
                .MaximumLength(50)
                .WithMessage("Brand must be 50 characters or fewer.");

            RuleFor(product => product.ShadeName)
                .NotEmpty()
                .WithMessage("Shade name is required.")
                .MaximumLength(50)
                .WithMessage("Shade name must be 50 characters or fewer.");
        }
    }
}
