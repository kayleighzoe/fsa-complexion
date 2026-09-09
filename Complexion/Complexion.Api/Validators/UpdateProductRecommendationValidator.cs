using Complexion.Models.Products;
using FluentValidation;

namespace Complexion.Api.Validators
{
    public class UpdateProductRecommendationValidator: AbstractValidator<UpdateProductRecommendationDto>
    {
        public UpdateProductRecommendationValidator()
        {
            RuleFor(productRecommendation => productRecommendation.Comment)
                .MaximumLength(255)
                .WithMessage("Comment must be 255 characters or fewer.")
                .When(productRecommendation => !string.IsNullOrWhiteSpace(productRecommendation.Comment));
        }
    }
}
