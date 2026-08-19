using Complexion.DTOs.Dbo;
using FluentValidation;

namespace Complexion.Api.Validators
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
