using Complexion.Models.Products;
using FluentValidation;

namespace Complexion.Api.Validators
{
    public class ShareRecommendationValidator : AbstractValidator<ShareRecommendationDto>
    {
        public ShareRecommendationValidator()
        {
            RuleFor(share => share.CategoryName)
                .NotEmpty().WithMessage("Category is required.");

            RuleFor(share => share.PriceTierName)
                .NotEmpty().WithMessage("Price tier is required.");

            RuleFor(share => share.Brand)
                .NotEmpty().WithMessage("Brand is required.");

            RuleFor(share => share.ProductName)
                .NotEmpty().WithMessage("Product name is required.");

            RuleFor(share => share.ShadeName)
                .NotEmpty().WithMessage("Shade name is required.");

            RuleFor(share => share.MatchedShadeName)
                .NotEmpty().WithMessage("Matched skin shade is required.");

            RuleFor(share => share.MatchedUndertoneName)
                .NotEmpty().WithMessage("Matched skin undertone is required.");

            RuleFor(share => share.Comment)
                .MaximumLength(255).WithMessage("Comment must be 255 characters or fewer.");
        }
    }
}