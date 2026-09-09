using Complexion.Api.Validators;
using Complexion.Models.Products;
using FluentValidation.TestHelper;

namespace Complexion.Tests.Validators
{
    public class UpdateProductRecommendationValidatorTests
    {
        private UpdateProductRecommendationValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new UpdateProductRecommendationValidator();
        }

        [Test]
        public void GIVEN_NullComment_WHEN_ValidatingUpdateProductRecommendation_THEN_ReturnNoValidationError()
        {
            //arrange
            var dto = new UpdateProductRecommendationDto
            {
                Comment = null
            };

            //act
            var result = _validator.TestValidate(dto);

            //assert
            result.ShouldNotHaveValidationErrorFor(x => x.Comment);
        }

        [Test]
        public void GIVEN_EmptyComment_WHEN_ValidatingUpdateProductRecommendation_THEN_ReturnNoValidationError()
        {
            //arrange
            var dto = new UpdateProductRecommendationDto
            {
                Comment = ""
            };

            //act
            var result = _validator.TestValidate(dto);

            //assert
            result.ShouldNotHaveValidationErrorFor(x => x.Comment);
        }

        [Test]
        public void GIVEN_CommentOver255Characters_WHEN_ValidatingUpdateProductRecommendation_THEN_ReturnValidationError()
        {
            //arrange
            var dto = new UpdateProductRecommendationDto
            {
                Comment = new string('a', 256)
            };
            var expectedMessage = "Comment must be 255 characters or fewer.";

            //act
            var result = _validator.TestValidate(dto);

            //assert
            result.ShouldHaveValidationErrorFor(x => x.Comment).WithErrorMessage(expectedMessage);
        }
    }
}
