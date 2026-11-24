using Complexion.Api.Validators;
using Complexion.Models.Auth;
using FluentValidation.TestHelper;

namespace Complexion.Tests
{
    public class LoginValidatorTests
    {
        private LoginValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new LoginValidator();
        }

        [Test]
        public void GIVEN_ValidDto_WHEN_ValidatingLogin_THEN_ReturnNoValidationErrors()
        {
            // Arrange
            var dto = new LoginDto
            {
                UsernameOrEmail = "test@example.com",
                Password = "Password123"
            };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void GIVEN_EmptyUsernameOrEmail_WHEN_ValidatingLogin_THEN_ReturnValidationError()
        {
            // Arrange
            var dto = new LoginDto
            {
                UsernameOrEmail = "",
                Password = "Password123"
            };
            var expectedMessage = "Username or email is required.";

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.UsernameOrEmail).WithErrorMessage(expectedMessage);
        }

        [Test]
        public void GIVEN_UsernameOrEmailOver50Characters_WHEN_ValidatingLogin_THEN_ReturnValidationError()
        {
            // Arrange
            var dto = new LoginDto
            {
                UsernameOrEmail = new string('a', 51),
                Password = "Password123"
            };
            var expectedMessage = "Username or email must be 50 characters or fewer.";

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.UsernameOrEmail).WithErrorMessage(expectedMessage);
        }

        [Test]
        public void GIVEN_EmptyPassword_WHEN_ValidatingLogin_THEN_ReturnValidationError()
        {
            // Arrange
            var dto = new LoginDto
            {
                UsernameOrEmail = "test@example.com",
                Password = ""
            };
            var expectedMessage = "Password is required.";

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Password).WithErrorMessage(expectedMessage);
        }

        [Test]
        public void GIVEN_PasswordOver64Characters_WHEN_ValidatingLogin_THEN_ReturnValidationError()
        {
            // Arrange
            var dto = new LoginDto
            {
                UsernameOrEmail = "test@example.com",
                Password = new string('a', 65)
            };
            var expectedMessage = "Password must be 64 characters or fewer.";

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Password).WithErrorMessage(expectedMessage);
        }
    }
}