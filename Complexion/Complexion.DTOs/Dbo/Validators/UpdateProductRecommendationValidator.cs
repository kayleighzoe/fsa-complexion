using FluentValidation;

namespace Complexion.DTOs.Dbo.Validators
{
    public class UpdateProductRecommendationValidator: AbstractValidator<UpdateProductRecommendationDto>
    {
        public UpdateProductRecommendationValidator()
        {
            RuleFor(x => x.Comment).MaximumLength(255);
        }
    }
}
