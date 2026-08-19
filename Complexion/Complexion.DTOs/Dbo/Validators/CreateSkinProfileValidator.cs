using FluentValidation;

namespace Complexion.DTOs.Dbo.Validators
{
    public class CreateSkinProfileValidator: AbstractValidator<CreateSkinProfileDto>
    {
        public CreateSkinProfileValidator()
        {
            RuleFor(x => x.ShadeId).NotEmpty().GreaterThan(0);

            RuleFor(x => x.UndertoneId).NotEmpty().GreaterThan(0);
        }
    }
}
