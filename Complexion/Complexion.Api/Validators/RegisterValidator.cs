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
                .Must(StartsWithLetter).WithMessage("Username must start with a letter.")
                .Must(ContainsOnlyLettersAndDigits).WithMessage("Username can only contain letters and numbers.");

            RuleFor(register => register.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(12).WithMessage("Password must be at least 12 characters.")
                .MaximumLength(64).WithMessage("Password must be 64 characters or fewer.")
                .Must(HasUppercaseLetter).WithMessage("Password must contain an uppercase letter.")
                .Must(HasLowercaseLetter).WithMessage("Password must contain a lowercase letter.")
                .Must(HasDigit).WithMessage("Password must contain a number.")
                .Must(HasSpecialCharacter).WithMessage("Password must contain a special character.");

            RuleFor(register => register.ShadeId)
                .GreaterThan(0)
                .WithMessage("ShadeId is required.");

            RuleFor(register => register.UndertoneId)
                .GreaterThan(0)
                .WithMessage("UndertoneId is required.");
        }

        private static bool StartsWithLetter(string? username) =>
            !string.IsNullOrEmpty(username) && char.IsAsciiLetter(username[0]);

        private static bool ContainsOnlyLettersAndDigits(string? username) =>
            !string.IsNullOrEmpty(username) && username.All(char.IsAsciiLetterOrDigit);

        private static bool HasUppercaseLetter(string? password) =>
            password is not null && password.Any(char.IsAsciiLetterUpper);

        private static bool HasLowercaseLetter(string? password) =>
            password is not null && password.Any(char.IsAsciiLetterLower);

        private static bool HasDigit(string? password) =>
            password is not null && password.Any(char.IsAsciiDigit);

        private static bool HasSpecialCharacter(string? password) =>
            password is not null && password.Any(character => !char.IsAsciiLetterOrDigit(character));
    }
}