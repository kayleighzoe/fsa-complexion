using Complexion.Models.Catalogue;
using Complexion.Repository.Catalogue;
using Complexion.Services.Catalogue;
using NSubstitute;

namespace Complexion.Tests.Services.Catalogue
{
    [TestFixture]
    public class CatalogueCategoryServiceTests
    {
        private ICatalogueCategoryRepository _repository;
        private CatalogueCategoryService _service;

        [SetUp]
        public void SetUp()
        {
            _repository = Substitute.For<ICatalogueCategoryRepository>();
            _service = new CatalogueCategoryService(_repository);
        }

        [Test]
        public async Task GivenCategoriesExistInRepository_WhenGetAllCategoriesAsyncIsCalled_ReturnsCategoriesFromRepository()
        {
            // Arrange
            var expectedCategories = new List<CatalogueCategory>
            {
                new CatalogueCategory { CategoryId = 1, Name = "High-coverage Foundation" },
                new CatalogueCategory { CategoryId = 2, Name = "Concealer" }
            };

            _repository.GetAllCategoriesAsync().Returns(expectedCategories);

            // Act
            var result = await _service.GetAllCategoriesAsync();

            // Assert
            Assert.That(result, Is.EqualTo(expectedCategories));
            await _repository.Received(1).GetAllCategoriesAsync();
        }
    }
}