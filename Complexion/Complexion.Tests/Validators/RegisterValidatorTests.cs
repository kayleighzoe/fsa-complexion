using Complexion.Api.Validators;
using Complexion.Models.Auth;
using FluentValidation.TestHelper;

namespace Complexion.Tests
{
    public class RegisterValidatorTests
    {
        private RegisterValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new RegisterValidator();
        }

        private static RegisterDto CreateValidDto()
        {
            return new RegisterDto
            {
                FirstName = "Test",
                Surname = "User",
                Email = "test@example.com",
                Username = "Test123",
                Password = "Password123!",
                ShadeId = 1,
                UndertoneId = 1
            };
        }

        [Test]
        public void GIVEN_ValidDto_WHEN_ValidatingRegister_THEN_ReturnNoValidationErrors()
        {
            // Arrange
            var dto = CreateValidDto();

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void GIVEN_EmptyFirstName_WHEN_ValidatingRegister_THEN_ReturnValidationError()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.FirstName = "";
            var expectedMessage = "First name is required.";

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.FirstName).WithErrorMessage(expectedMessage);
        }

        [Test]
        public void GIVEN_FirstNameOver50Characters_WHEN_ValidatingRegister_THEN_ReturnValidationError()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.FirstName = new string('a', 51);
            var expectedMessage = "First name must be 50 characters or fewer.";

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.FirstName).WithErrorMessage(expectedMessage);
        }

        [Test]
        public void GIVEN_EmptySurname_WHEN_ValidatingRegister_THEN_ReturnValidationError()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.Surname = "";
            var expectedMessage = "Surname is required.";

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Surname).WithErrorMessage(expectedMessage);
        }

        [Test]
        public void GIVEN_SurnameOver50Characters_WHEN_ValidatingRegister_THEN_ReturnValidationError()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.Surname = new string('a', 51);
            var expectedMessage = "Surname must be 50 characters or fewer.";

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Surname).WithErrorMessage(expectedMessage);
        }

        [Test]
        public void GIVEN_EmptyEmail_WHEN_ValidatingRegister_THEN_ReturnValidationError()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.Email = "";
            var expectedMessage = "Email is required.";

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Email).WithErrorMessage(expectedMessage);
        }

        [Test]
        public void GIVEN_EmailInInvalidFormat_WHEN_ValidatingRegister_THEN_ReturnValidationError()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.Email = "not-an-email";
            var expectedMessage = "Email must be a valid email address.";

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Email).WithErrorMessage(expectedMessage);
        }

        [Test]
        public void GIVEN_EmailOver50Characters_WHEN_ValidatingRegister_THEN_ReturnValidationError()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.Email = new string('a', 40) + "@example.com";
            var expectedMessage = "Email must be 50 characters or fewer.";

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Email).WithErrorMessage(expectedMessage);
        }

        [Test]
        public void GIVEN_EmptyUsername_WHEN_ValidatingRegister_THEN_ReturnValidationError()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.Username = "";
            var expectedMessage = "Username is required.";

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Username).WithErrorMessage(expectedMessage);
        }

        [Test]
        public void GIVEN_UsernameUnder5Characters_WHEN_ValidatingRegister_THEN_ReturnValidationError()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.Username = "Test";
            var expectedMessage = "Username must be at least 5 characters.";

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Username).WithErrorMessage(expectedMessage);
        }

        [Test]
        public void GIVEN_UsernameOver50Characters_WHEN_ValidatingRegister_THEN_ReturnValidationError()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.Username = new string('a', 51);
            var expectedMessage = "Username must be 50 characters or fewer.";

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Username).WithErrorMessage(expectedMessage);
        }

        [Test]
        public void GIVEN_UsernameStartingWithADigit_WHEN_ValidatingRegister_THEN_ReturnValidationError()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.Username = "1TestU";
            var expectedMessage = "Username must start with a letter.";

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Username).WithErrorMessage(expectedMessage);
        }

        [Test]
        public void GIVEN_UsernameWithASpecialCharacter_WHEN_ValidatingRegister_THEN_ReturnValidationError()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.Username = "Test_123";
            var expectedMessage = "Username can only contain letters and numbers.";

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Username).WithErrorMessage(expectedMessage);
        }

        [Test]
        public void GIVEN_EmptyPassword_WHEN_ValidatingRegister_THEN_ReturnValidationError()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.Password = "";
            var expectedMessage = "Password is required.";

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Password).WithErrorMessage(expectedMessage);
        }

        [Test]
        public void GIVEN_PasswordUnder12Characters_WHEN_ValidatingRegister_THEN_ReturnValidationError()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.Password = "Pass123!";
            var expectedMessage = "Password must be at least 12 characters.";

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Password).WithErrorMessage(expectedMessage);
        }

        [Test]
        public void GIVEN_PasswordOver64Characters_WHEN_ValidatingRegister_THEN_ReturnValidationError()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.Password = "Aa1!" + new string('a', 61);
            var expectedMessage = "Password must be 64 characters or fewer.";

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Password).WithErrorMessage(expectedMessage);
        }

        [Test]
        public void GIVEN_PasswordWithoutAnUppercaseLetter_WHEN_ValidatingRegister_THEN_ReturnValidationError()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.Password = "password123!";
            var expectedMessage = "Password must contain an uppercase letter.";

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Password).WithErrorMessage(expectedMessage);
        }

        [Test]
        public void GIVEN_PasswordWithoutALowercaseLetter_WHEN_ValidatingRegister_THEN_ReturnValidationError()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.Password = "PASSWORD123!";
            var expectedMessage = "Password must contain a lowercase letter.";

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Password).WithErrorMessage(expectedMessage);
        }

        [Test]
        public void GIVEN_PasswordWithoutANumber_WHEN_ValidatingRegister_THEN_ReturnValidationError()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.Password = "Password!!!!";
            var expectedMessage = "Password must contain a number.";

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Password).WithErrorMessage(expectedMessage);
        }

        [Test]
        public void GIVEN_PasswordWithoutASpecialCharacter_WHEN_ValidatingRegister_THEN_ReturnValidationError()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.Password = "Password1234";
            var expectedMessage = "Password must contain a special character.";

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Password).WithErrorMessage(expectedMessage);
        }

        [Test]
        public void GIVEN_ShadeIdOfZero_WHEN_ValidatingRegister_THEN_ReturnValidationError()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.ShadeId = 0;
            var expectedMessage = "ShadeId is required.";

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.ShadeId).WithErrorMessage(expectedMessage);
        }

        [Test]
        public void GIVEN_UndertoneIdOfZero_WHEN_ValidatingRegister_THEN_ReturnValidationError()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.UndertoneId = 0;
            var expectedMessage = "UndertoneId is required.";

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.UndertoneId).WithErrorMessage(expectedMessage);
        }
    }
}