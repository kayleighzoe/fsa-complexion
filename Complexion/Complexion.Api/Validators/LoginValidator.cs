using Complexion.Models.Auth;
using FluentValidation;

namespace Complexion.Api.Validators
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(x => x.UsernameOrEmail)
                .NotEmpty().WithMessage("Username or email is required.")
                .MaximumLength(50).WithMessage("Username or email must be 50 characters or fewer.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MaximumLength(64).WithMessage("Password must be 64 characters or fewer.");
        }
    }
}