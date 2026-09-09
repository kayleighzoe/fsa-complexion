using Complexion.Models.Dbo;
using FluentValidation;

namespace Complexion.Api.Validators
{
    public class CreateProductRecommendationsValidator : AbstractValidator<CreateProductRecommendationDto>
    {
        public CreateProductRecommendationsValidator()
        {
            RuleFor(productRecommendation => productRecommendation.UserId)
                .NotEmpty()
                .WithMessage("UserId is required.");

            RuleFor(productRecommendation => productRecommendation.ProductId)
                .NotEmpty()
                .WithMessage("ProductId is required.");

            RuleFor(productRecommendation => productRecommendation.SkinProfileId)
                .NotEmpty()
                .WithMessage("SkinProfileId is required.");

            RuleFor(productRecommendation => productRecommendation.Comment)
                .MaximumLength(255)
                .WithMessage("Comment must be 255 characters or fewer.")
                .When(productRecommendation => !string.IsNullOrWhiteSpace(productRecommendation.Comment));
        }
    }
}
