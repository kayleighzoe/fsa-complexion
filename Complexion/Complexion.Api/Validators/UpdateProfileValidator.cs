using Complexion.Models.Auth;
using FluentValidation;

namespace Complexion.Api.Validators
{
    public class UpdateProfileValidator : AbstractValidator<UpdateProfileDto>
    {
        public UpdateProfileValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name must be 50 characters or fewer.");

            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Surname is required.")
                .MaximumLength(50).WithMessage("Surname must be 50 characters or fewer.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email must be a valid email address.")
                .MaximumLength(50).WithMessage("Email must be 50 characters or fewer.");

            RuleFor(x => x.ShadeId)
                .GreaterThan(0).WithMessage("ShadeId is required.");

            RuleFor(x => x.UndertoneId)
                .GreaterThan(0).WithMessage("UndertoneId is required.");
        }
    }
}