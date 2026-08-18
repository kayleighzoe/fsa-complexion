using Complexion.Models.Catalogue;
using Complexion.Repository.Catalogue;
using Complexion.Services.Catalogue;
using NSubstitute;

namespace Complexion.Tests.Services.Catalogue
{
    public class CatalogueCategoryServiceTests
    {
        private ICatalogueCategoryRepository _catalogueCategoryRepository;
        private CatalogueCategoryService _catalogueCategoryService;

        [SetUp]
        public void SetUp()
        {
            _catalogueCategoryRepository = Substitute.For<ICatalogueCategoryRepository>();
            _catalogueCategoryService = new CatalogueCategoryService(_catalogueCategoryRepository);
        }

        [Test]
        public async Task GivenCategoriesExistInRepository_WhenGetAllCategoriesAsync_ReturnsCategoriesFromRepository()
        {
            // Arrange
            var expectedCategories = new List<CatalogueCategory>
            {
                new CatalogueCategory { CategoryId = 1, Name = "High-coverage Foundation" },
                new CatalogueCategory { CategoryId = 2, Name = "Concealer" }
            };

            _catalogueCategoryRepository.GetAllCategoriesAsync().Returns(expectedCategories);

            // Act
            var result = await _catalogueCategoryService.GetAllCategoriesAsync();

            // Assert
            Assert.That(result, Is.EqualTo(expectedCategories));
            await _catalogueCategoryRepository.Received(1).GetAllCategoriesAsync();
        }
    }
}