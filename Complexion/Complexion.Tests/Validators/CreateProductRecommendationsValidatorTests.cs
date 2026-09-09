using Complexion.Api.Validators;
using Complexion.Models.Products;
using FluentValidation.TestHelper;

namespace Complexion.Tests
{
    public class CreateProductRecommendationsValidatorTests
    {
        private CreateProductRecommendationsValidator _validator;
        
        [SetUp]
        public void Setup()
        {
            _validator = new CreateProductRecommendationsValidator();
        }


        [Test]
        public void GIVEN_EmptyUserId_WHEN_ValidatingCreateProductRecommendation_THEN_ReturnValidationError()
        {
            //arrange
            var dto = new CreateProductRecommendationDto 
            { 
                UserId = Guid.Empty, 
                ProductId = Guid.NewGuid(), 
                SkinProfileId = Guid.NewGuid() 
            };
            var expectedMessage = "UserId is required.";

            //act
            var result = _validator.TestValidate(dto);

            //assert
            result.ShouldHaveValidationErrorFor(x => x.UserId).WithErrorMessage(expectedMessage);
        }

        [Test]
        public void GIVEN_NullComment_WHEN_ValidatingCreateProductRecommendation_THEN_ReturnNoValidationError()
        {
            //arrange
            var dto = new CreateProductRecommendationDto
            {
                UserId = Guid.NewGuid(),
                ProductId = Guid.NewGuid(),
                SkinProfileId = Guid.NewGuid(),
                Comment = null
            };

            //act
            var result = _validator.TestValidate(dto);

            //assert
            result.ShouldNotHaveValidationErrorFor(x => x.Comment);
        }

        [Test]
        public void GIVEN_EmptyComment_WHEN_ValidatingCreateProductRecommendation_THEN_ReturnNoValidationError()
        {
            //arrange
            var dto = new CreateProductRecommendationDto
            {
                UserId = Guid.NewGuid(),
                ProductId = Guid.NewGuid(),
                SkinProfileId = Guid.NewGuid(),
                Comment = ""
            };

            //act
            var result = _validator.TestValidate(dto);

            //assert
            result.ShouldNotHaveValidationErrorFor(x => x.Comment);
        }

        [Test]
        public void GIVEN_CommentOver255Characters_WHEN_ValidatingCreateProductRecommendation_THEN_ReturnValidationError()
        {
            //arrange
            var dto = new CreateProductRecommendationDto
            {
                UserId = Guid.NewGuid(),
                ProductId = Guid.NewGuid(),
                SkinProfileId = Guid.NewGuid(),
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
