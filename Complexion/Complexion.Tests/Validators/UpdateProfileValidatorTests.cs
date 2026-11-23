using Complexion.Api.Validators;
using Complexion.Models.Auth;
using FluentValidation.TestHelper;

namespace Complexion.Tests
{
    public class UpdateProfileValidatorTests
    {
        private UpdateProfileValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new UpdateProfileValidator();
        }

        private static UpdateProfileDto CreateValidDto()
        {
            return new UpdateProfileDto
            {
                FirstName = "Test",
                Surname = "User",
                Email = "test@example.com",
                ShadeId = 1,
                UndertoneId = 1
            };
        }

        [Test]
        public void GIVEN_ValidDto_WHEN_ValidatingUpdateProfile_THEN_ReturnNoValidationErrors()
        {
            // Arrange
            var dto = CreateValidDto();

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void GIVEN_EmptyFirstName_WHEN_ValidatingUpdateProfile_THEN_ReturnValidationError()
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
        public void GIVEN_FirstNameOver50Characters_WHEN_ValidatingUpdateProfile_THEN_ReturnValidationError()
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
        public void GIVEN_EmptySurname_WHEN_ValidatingUpdateProfile_THEN_ReturnValidationError()
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
        public void GIVEN_SurnameOver50Characters_WHEN_ValidatingUpdateProfile_THEN_ReturnValidationError()
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
        public void GIVEN_EmptyEmail_WHEN_ValidatingUpdateProfile_THEN_ReturnValidationError()
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
        public void GIVEN_EmailInInvalidFormat_WHEN_ValidatingUpdateProfile_THEN_ReturnValidationError()
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
        public void GIVEN_EmailOver50Characters_WHEN_ValidatingUpdateProfile_THEN_ReturnValidationError()
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
        public void GIVEN_ShadeIdOfZero_WHEN_ValidatingUpdateProfile_THEN_ReturnValidationError()
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
        public void GIVEN_UndertoneIdOfZero_WHEN_ValidatingUpdateProfile_THEN_ReturnValidationError()
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