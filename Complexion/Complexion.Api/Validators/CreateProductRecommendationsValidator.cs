using Complexion.DTOs.Dbo;
using FluentValidation;

namespace Complexion.Api.Validators
{
    public class CreateProductRecommendationsValidator : AbstractValidator<CreateProductRecommendationDto>
    {
        public CreateProductRecommendationsValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();

            RuleFor(x => x.ProductId).NotEmpty();

            RuleFor(x => x.SkinProfileId).NotEmpty();

            RuleFor(x => x.Comment).MaximumLength(255).When(x => !string.IsNullOrEmpty(x.Comment));
        }
    }
}
