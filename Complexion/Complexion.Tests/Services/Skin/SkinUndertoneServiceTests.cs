using Complexion.Models.Skin;
using Complexion.Repository.Skin;
using Complexion.Services.Skin;
using NSubstitute;

namespace Complexion.Tests.Services.Skin
{ 
    public class SkinUndertoneServiceTests
    {
        private ISkinUndertoneRepository _skinUndertoneRepository;
        private SkinUndertoneService _skinUndertoneService;

        [SetUp]
        public void Setup()
        {
            _skinUndertoneRepository = Substitute.For<ISkinUndertoneRepository>();
            _skinUndertoneService = new SkinUndertoneService(_skinUndertoneRepository);
        }

        [Test]
        public async Task GivenUndertonesExistInRepository_WhenGetAllUndertonesAsync_ReturnsUndertonesFromRepository()
        {
            // Arrange
            var expectedUndertones = new List<SkinUndertone>
            {
                new SkinUndertone(),
                new SkinUndertone()
            };

            _skinUndertoneRepository.GetAllUndertonesAsync().Returns(expectedUndertones);

            // Act
            var result = await _skinUndertoneService.GetAllUndertonesAsync();

            // Assert
            Assert.That(result, Is.EqualTo(expectedUndertones));
            await _skinUndertoneRepository.Received(1).GetAllUndertonesAsync();
        }
    }
}
