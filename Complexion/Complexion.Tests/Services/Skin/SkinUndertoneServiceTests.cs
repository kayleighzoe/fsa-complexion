using Complexion.Models.Skin;
using Complexion.Repository.Skin;
using Complexion.Services.Skin;
using NSubstitute;

namespace Complexion.Tests.Services.Skin
{ 
    public class SkinUndertoneServiceTests
    {
        private ISkinUndertoneRepository _repository;
        private SkinUndertoneService _service;

        [SetUp]
        public void Setup()
        {
            _repository = Substitute.For<ISkinUndertoneRepository>();
            _service = new SkinUndertoneService(_repository);
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

            _repository.GetAllUndertonesAsync().Returns(expectedUndertones);

            // Act
            var result = await _service.GetAllUndertonesAsync();

            // Assert
            Assert.That(result, Is.EqualTo(expectedUndertones));
            await _repository.Received(1).GetAllUndertonesAsync();
        }
    }
}
