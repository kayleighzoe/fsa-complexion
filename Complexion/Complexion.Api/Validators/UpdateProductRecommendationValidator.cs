using Complexion.DTOs.Dbo;
using FluentValidation;

namespace Complexion.Api.Validators
{
    public class UpdateProductRecommendationValidator: AbstractValidator<UpdateProductRecommendationDto>
    {
        public UpdateProductRecommendationValidator()
        {
            RuleFor(x => x.Comment).MaximumLength(255);
        }
    }
}
