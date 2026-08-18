using Complexion.Models.Config;
using Complexion.Services.Config;
using Complexion.Repository.Config;
using NSubstitute;

namespace Complexion.Tests.Services.Config
{
    public class ConfigPriceTierServiceTests
    {
        private IConfigPriceTierRepository _repository;
        private ConfigPriceTierService _service;

        [SetUp]
        public void Setup()
        {
            _repository = Substitute.For<IConfigPriceTierRepository>();
            _service = new ConfigPriceTierService(_repository);
        }

        [Test]
        public async Task GivenPriceTiersExistInRepository_WhenGetAllPriceTiersAsyncIsCalled_ReturnsPriceTiersFromRepository()
        {   
            // Arrange
            var expectedPriceTiers = new List<ConfigPriceTier>
            {
                new ConfigPriceTier(),
                new ConfigPriceTier()
            };

            _repository.GetAllPriceTiersAsync().Returns(expectedPriceTiers);

            // Act
            var result = await _service.GetAllPriceTiersAsync();

            // Assert
            Assert.That(result, Is.EqualTo(expectedPriceTiers));
            await _repository.Received(1).GetAllPriceTiersAsync();
        }
    }
}
