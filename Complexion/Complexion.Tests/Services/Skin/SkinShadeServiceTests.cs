using Complexion.Models.Skin;
using Complexion.Repository.Skin;
using Complexion.Services.Skin;
using NSubstitute;

namespace Complexion.Tests.Services.Skin
{
    public class SkinShadeServiceTests
    {
        private ISkinShadeRepository _skinShadeRepository;
        private SkinShadeService _skinShadeService;

        [SetUp]
        public void Setup()
        {
            _skinShadeRepository = Substitute.For<ISkinShadeRepository>();
            _skinShadeService = new SkinShadeService(_skinShadeRepository);
        }

        [Test]
        public async Task GivenSkinShadesExistInRepository_WhenGetAllSkinShadesAsync_ReturnsShadesFromRepository()
        {
            // Arrange
            var expectedShades = new List<SkinShade>
            {
                new SkinShade(),
                new SkinShade()
            };

            _skinShadeRepository.GetAllSkinShadesAsync().Returns(expectedShades);

            // Act
            var result = await _skinShadeService.GetAllSkinShadesAsync();

            // Assert
            Assert.That(result, Is.EqualTo(expectedShades));
            await _skinShadeRepository.Received(1).GetAllSkinShadesAsync();
        }
    }
}
