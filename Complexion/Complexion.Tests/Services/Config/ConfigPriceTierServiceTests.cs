using Complexion.Models.Config;
using Complexion.Services.Config;
using Complexion.Repository.Config;
using NSubstitute;

namespace Complexion.Tests.Services.Config
{
    public class ConfigPriceTierServiceTests
    {
        private IConfigPriceTierRepository _priceTierRepository;
        private ConfigPriceTierService _priceTierService;

        [SetUp]
        public void Setup()
        {
            _priceTierRepository = Substitute.For<IConfigPriceTierRepository>();
            _priceTierService = new ConfigPriceTierService(_priceTierRepository);
        }

        [Test]
        public async Task GivenPriceTiersExistInRepository_WhenGetAllPriceTiersAsync_ReturnsPriceTiersFromRepository()
        {   
            // Arrange
            var expectedPriceTiers = new List<ConfigPriceTier>
            {
                new ConfigPriceTier(),
                new ConfigPriceTier()
            };

            _priceTierRepository.GetAllPriceTiersAsync().Returns(expectedPriceTiers);

            // Act
            var result = await _priceTierService.GetAllPriceTiersAsync();

            // Assert
            Assert.That(result, Is.EqualTo(expectedPriceTiers));
            await _priceTierRepository.Received(1).GetAllPriceTiersAsync();
        }
    }
}
