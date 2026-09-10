using Complexion.Models.Skin;
using FluentValidation;

namespace Complexion.Api.Validators
{
    public class CreateSkinProfileValidator : AbstractValidator<CreateSkinProfileDto>
    {
        public CreateSkinProfileValidator()
        {
            RuleFor(skinProfile => skinProfile.ShadeId)
                .GreaterThan(0)
                .WithMessage("ShadeId is required.");

            RuleFor(skinProfile => skinProfile.UndertoneId)
                .GreaterThan(0)
                .WithMessage("UndertoneId is required.");
        }
    }
}
