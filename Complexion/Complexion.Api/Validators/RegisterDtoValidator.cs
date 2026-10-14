using Complexion.Models.Auth;
using FluentValidation;

namespace Complexion.Api.Validators
{
    public class RegisterValidator : AbstractValidator<RegisterDto>
    {
        public RegisterValidator()
        {
            RuleFor(register => register.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name must be 50 characters or fewer.");

            RuleFor(register => register.Surname)
                .NotEmpty().WithMessage("Surname is required.")
                .MaximumLength(50).WithMessage("Surname must be 50 characters or fewer.");

            RuleFor(register => register.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email must be a valid email address.")
                .MaximumLength(50).WithMessage("Email must be 50 characters or fewer.");

            RuleFor(register => register.Username)
                .NotEmpty().WithMessage("Username is required.")
                .MinimumLength(5).WithMessage("Username must be at least 5 characters.")
                .MaximumLength(50).WithMessage("Username must be 50 characters or fewer.")
                .Matches("^[A-Za-z]").WithMessage("Username must start with a letter.")
                .Matches("^[A-Za-z0-9]+$").WithMessage("Username can only contain letters and numbers.");

            RuleFor(register => register.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(12).WithMessage("Password must be at least 12 characters.")
                .MaximumLength(64).WithMessage("Password must be 64 characters or fewer.")
                .Matches("[A-Z]").WithMessage("Password must contain an uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain a lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain a number.")
                .Matches("[^A-Za-z0-9]").WithMessage("Password must contain a special character.");

            RuleFor(register => register.ShadeId)
                .GreaterThan(0)
                .WithMessage("ShadeId is required.");

            RuleFor(register => register.UndertoneId)
                .GreaterThan(0)
                .WithMessage("UndertoneId is required.");
        }
    }
}