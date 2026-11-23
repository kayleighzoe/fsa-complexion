using Complexion.Api.Validators;
using Complexion.Models.Products;
using FluentValidation.TestHelper;

namespace Complexion.Tests
{
    public class ShareRecommendationValidatorTests
    {
        private ShareRecommendationValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new ShareRecommendationValidator();
        }

        private static ShareRecommendationDto CreateValidDto()
        {
            return new ShareRecommendationDto
            {
                CategoryName = "Foundation",
                PriceTierName = "Mid-range",
                Brand = "Test Brand",
                ProductName = "Test Product",
                ShadeName = "Test Shade",
                MatchedShadeName = "Medium",
                MatchedUndertoneName = "Warm",
                Comment = "Great match"
            };
        }

        [Test]
        public void GIVEN_ValidDto_WHEN_ValidatingShareRecommendation_THEN_ReturnNoValidationErrors()
        {
            // Arrange
            var dto = CreateValidDto();

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void GIVEN_EmptyCategoryName_WHEN_ValidatingShareRecommendation_THEN_ReturnValidationError()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.CategoryName = "";
            var expectedMessage = "Category is required.";

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.CategoryName).WithErrorMessage(expectedMessage);
        }

        [Test]
        public void GIVEN_EmptyPriceTierName_WHEN_ValidatingShareRecommendation_THEN_ReturnValidationError()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.PriceTierName = "";
            var expectedMessage = "Price tier is required.";

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.PriceTierName).WithErrorMessage(expectedMessage);
        }

        [Test]
        public void GIVEN_EmptyBrand_WHEN_ValidatingShareRecommendation_THEN_ReturnValidationError()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.Brand = "";
            var expectedMessage = "Brand is required.";

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Brand).WithErrorMessage(expectedMessage);
        }

        [Test]
        public void GIVEN_EmptyProductName_WHEN_ValidatingShareRecommendation_THEN_ReturnValidationError()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.ProductName = "";
            var expectedMessage = "Product name is required.";

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.ProductName).WithErrorMessage(expectedMessage);
        }

        [Test]
        public void GIVEN_EmptyShadeName_WHEN_ValidatingShareRecommendation_THEN_ReturnValidationError()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.ShadeName = "";
            var expectedMessage = "Shade name is required.";

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.ShadeName).WithErrorMessage(expectedMessage);
        }

        [Test]
        public void GIVEN_EmptyMatchedShadeName_WHEN_ValidatingShareRecommendation_THEN_ReturnValidationError()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.MatchedShadeName = "";
            var expectedMessage = "Matched skin shade is required.";

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.MatchedShadeName).WithErrorMessage(expectedMessage);
        }

        [Test]
        public void GIVEN_EmptyMatchedUndertoneName_WHEN_ValidatingShareRecommendation_THEN_ReturnValidationError()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.MatchedUndertoneName = "";
            var expectedMessage = "Matched skin undertone is required.";

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.MatchedUndertoneName).WithErrorMessage(expectedMessage);
        }

        [Test]
        public void GIVEN_CommentOver255Characters_WHEN_ValidatingShareRecommendation_THEN_ReturnValidationError()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.Comment = new string('a', 256);
            var expectedMessage = "Comment must be 255 characters or fewer.";

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Comment).WithErrorMessage(expectedMessage);
        }

        [Test]
        public void GIVEN_NullComment_WHEN_ValidatingShareRecommendation_THEN_ReturnNoValidationError()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.Comment = null;

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Comment);
        }
    }
}