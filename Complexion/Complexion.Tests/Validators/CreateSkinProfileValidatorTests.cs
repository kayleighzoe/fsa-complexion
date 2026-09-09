using Complexion.Api.Validators;
using Complexion.Models.Skin;
using FluentValidation.TestHelper;

namespace Complexion.Tests.Validators
{
    public class CreateSkinProfileValidatorTests
    {
        private CreateSkinProfileValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new CreateSkinProfileValidator();
        }

        [Test]
        public void GIVEN_ZeroShadeId_WHEN_ValidatingCreateSkinProfile_THEN_ReturnValidationError()
        {
            //arrange
            var dto = new CreateSkinProfileDto
            {
                ShadeId = 0,
                UndertoneId = 1,
                HasTint = true,
            };
            var expectedMessage = "ShadeId is required.";

            //act
            var result = _validator.TestValidate(dto);

            //assert
            result.ShouldHaveValidationErrorFor(x => x.ShadeId).WithErrorMessage(expectedMessage);
        }

        [Test]
        public void GIVEN_AllCorrectValues_WHEN_ValidatingCreateSkinProfile_THEN_ReturnNoValidationError()
        {
            //arrange
            var dto = new CreateSkinProfileDto
            {
                ShadeId = 1,
                UndertoneId = 1,
                HasTint = true,
            };

            //act
            var result = _validator.TestValidate(dto);

            //assert
            Assert.Multiple(() =>
            {
                result.ShouldNotHaveValidationErrorFor(x => x.ShadeId);
                result.ShouldNotHaveValidationErrorFor(x => x.UndertoneId);
            });
            
        }

        [Test]
        public void GIVEN_ZeroUndertoneId_WHEN_ValidatingCreateSkinProfile_THEN_ReturnValidationError()
        {
            //arrange
            var dto = new CreateSkinProfileDto
            {
                ShadeId = 1,
                UndertoneId = 0,
                HasTint = true,
            };
            var expectedMessage = "UndertoneId is required.";

            //act
            var result = _validator.TestValidate(dto);

            //assert
            result.ShouldHaveValidationErrorFor(x => x.UndertoneId).WithErrorMessage(expectedMessage);
        }
    }
}
