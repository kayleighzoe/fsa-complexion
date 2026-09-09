using Complexion.Api.Validators;
using Complexion.Models.Products;
using FluentValidation.TestHelper;

namespace Complexion.Tests.Validators
{
    public class CreateProductValidatorTests
    {
        private CreateProductValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new CreateProductValidator();
        }

        [Test]
        public void GIVEN_ZeroCategoryId_WHEN_ValidatingCreateProduct_THEN_ReturnValidationError()
        {
            //arrange
            var dto = new CreateProductDto
            {
                CategoryId = 0,
                PriceTierId = 1,
                Name = "Foundation",
                Brand = "MAC",
                ShadeName = "NC44.5"
            };
            var expectedMessage = "CategoryId is required.";

            //act
            var result = _validator.TestValidate(dto);

            //assert
            result.ShouldHaveValidationErrorFor(x => x.CategoryId).WithErrorMessage(expectedMessage);
        }

        [Test]
        public void GIVEN_AllCorrectValues_WHEN_ValidatingCreateProduct_THEN_ReturnNoValidationError()
        {
            //arrange
            var dto = new CreateProductDto
            {
                CategoryId = 1,
                PriceTierId = 1,
                Name = "Foundation",
                Brand = "MAC",
                ShadeName = "NC44.5"
            };

            //act
            var result = _validator.TestValidate(dto);

            //assert
            Assert.Multiple(() =>
            {
                result.ShouldNotHaveValidationErrorFor(x => x.CategoryId);
                result.ShouldNotHaveValidationErrorFor(x => x.PriceTierId);
                result.ShouldNotHaveValidationErrorFor(x => x.Name);
                result.ShouldNotHaveValidationErrorFor(x => x.Brand);
                result.ShouldNotHaveValidationErrorFor(x => x.ShadeName);
            });
        }

        [Test]
        public void GIVEN_ZeroPriceTierId_WHEN_ValidatingCreateProduct_THEN_ReturnValidationError()
        {
            //arrange
            var dto = new CreateProductDto
            {
                CategoryId = 1,
                PriceTierId = 0,
                Name = "Foundation",
                Brand = "MAC",
                ShadeName = "NC44.5"
            };
            var expectedMessage = "PriceTierId is required.";

            //act
            var result = _validator.TestValidate(dto);

            //assert
            result.ShouldHaveValidationErrorFor(x => x.PriceTierId).WithErrorMessage(expectedMessage);
        }

        [Test]
        public void GIVEN_NameOver50Characters_WHEN_ValidatingCreateProduct_THEN_ReturnValidationError()
        {
            //arrange
            var dto = new CreateProductDto
            {
                CategoryId = 1,
                PriceTierId = 1,
                Name = new string('a', 51),
                Brand = "MAC",
                ShadeName = "NC44.5"
            };
            var expectedMessage = "Product name must be 50 characters or fewer.";

            //act
            var result = _validator.TestValidate(dto);

            //assert
            result.ShouldHaveValidationErrorFor(x => x.Name).WithErrorMessage(expectedMessage);
        }

        [Test]
        public void GIVEN_BrandOver50Characters_WHEN_ValidatingCreateProduct_THEN_ReturnValidationError()
        {
            //arrange
            var dto = new CreateProductDto
            {
                CategoryId = 1,
                PriceTierId = 1,
                Name = "Foundation",
                Brand = new string('a', 51),
                ShadeName = "NC44.5"
            };
            var expectedMessage = "Brand must be 50 characters or fewer.";

            //act
            var result = _validator.TestValidate(dto);

            //assert
            result.ShouldHaveValidationErrorFor(x => x.Brand).WithErrorMessage(expectedMessage);
        }

        [Test]
        public void GIVEN_ShadeNameOver50Characters_WHEN_ValidatingCreateProduct_THEN_ReturnValidationError()
        {
            //arrange
            var dto = new CreateProductDto
            {
                CategoryId = 1,
                PriceTierId = 1,
                Name = "Foundation",
                Brand = "MAC",
                ShadeName = new string('a', 51)
            };
            var expectedMessage = "Shade name must be 50 characters or fewer.";

            //act
            var result = _validator.TestValidate(dto);

            //assert
            result.ShouldHaveValidationErrorFor(x => x.ShadeName).WithErrorMessage(expectedMessage);
        }
    }
}
