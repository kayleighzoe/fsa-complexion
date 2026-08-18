using Complexion.Models.Skin;
using Complexion.Repository.Skin;
using Complexion.Services.Skin;
using NSubstitute;

namespace Complexion.Tests.Services.Skin
{
    public class SkinShadeServiceTests
    {
        private ISkinShadeRepository _repository;
        private SkinShadeService _service;

        [SetUp]
        public void Setup()
        {
            _repository = Substitute.For<ISkinShadeRepository>();
            _service = new SkinShadeService(_repository);
        }

        [Test]
        public async Task GivenSkinShadesExistInRepository_WhenGetAllSkinShadesAsyncIsCalled_ReturnsShadesFromRepository()
        {
            // Arrange
            var expectedShades = new List<SkinShade>
            {
                new SkinShade(),
                new SkinShade()
            };

            _repository.GetAllSkinShadesAsync().Returns(expectedShades);

            // Act
            var result = await _service.GetAllSkinShadesAsync();

            // Assert
            Assert.That(result, Is.EqualTo(expectedShades));
            await _repository.Received(1).GetAllSkinShadesAsync();
        }
    }
}
