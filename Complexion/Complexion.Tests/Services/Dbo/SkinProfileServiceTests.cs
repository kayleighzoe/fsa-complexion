using Complexion.DTOs.Dbo;
using Complexion.Models.Dbo;
using Complexion.Repository.Dbo;
using Complexion.Services.Dbo;
using NSubstitute;

namespace Complexion.Tests.Services.Dbo
{
    public class SkinProfileServiceTests
    {
        private ISkinProfileRepository _skinProfileRepository;
        private SkinProfileService _skinProfileService;

        [SetUp]
        public void Setup()
        {
            _skinProfileRepository = Substitute.For<ISkinProfileRepository>();
            _skinProfileService  = new SkinProfileService(_skinProfileRepository);     
        }

        [Test]
        public async Task GivenSkinProfilesExistInRepository_WhenGetAllSkinProfilesAsync_ReturnsSkinProfilesFromRepository()
        {
            // Arrange
            var expectedProfiles = new List<SkinProfile>
            {
                new SkinProfile
                {
                    SkinProfileId = Guid.NewGuid(),
                    ShadeId = 1,
                    UndertoneId = 1,
                    HasTint = false
                },
                new SkinProfile
                {
                    SkinProfileId = Guid.NewGuid(),
                    ShadeId = 2,
                    UndertoneId = 3,
                    HasTint = true
                }
            };

            _skinProfileRepository.GetAllSkinProfilesAsync().Returns(expectedProfiles);

            // Act
            var result = await _skinProfileService.GetAllSkinProfilesAsync();

            // Assert
            Assert.That(result, Is.EqualTo(expectedProfiles));
            await _skinProfileRepository.Received(1).GetAllSkinProfilesAsync();
        }

        [Test]
        public async Task GivenSkinProfileExistsInRepository_WhenGetSkinProfilesByIdAsync_ReturnsSkinProfileFromRepository()
        {
            // Arrange
            var id = Guid.NewGuid();
            var expectedProfile = new SkinProfile
            {
                SkinProfileId = id,
                ShadeId = 3,
                UndertoneId = 2,
                HasTint = true
            };

            _skinProfileRepository.GetSkinProfilesByIdAsync(id).Returns(expectedProfile);

            // Act
            var result = await _skinProfileService.GetSkinProfilesByIdAsync(id);

            // Assert
            Assert.That(result, Is.EqualTo(expectedProfile));
            await _skinProfileRepository.Received(1).GetSkinProfilesByIdAsync(id);
        }

        [Test]
        public void GivenSkinProfileDoesNotExistInRepository_WhenGetSkinProfilesByIdAsync_ThrowsKeyNotFoundException()
        {
            // Arrange
            var id = Guid.NewGuid();
            _skinProfileRepository.GetSkinProfilesByIdAsync(id).Returns((SkinProfile?)null);

            // Act & Assert
            Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                await _skinProfileService.GetSkinProfilesByIdAsync(id));
        }

        [Test]
        public async Task GivenValidDto_WhenCreateSkinProfileAsync_CallsRepositoryCreate()
        {
            // Arrange
            var dto = new CreateSkinProfileDto
            {
                ShadeId = 4,
                UndertoneId = 5,
                HasTint = false
            };

            // Act
            await _skinProfileService.CreateSkinProfileAsync(dto);

            // Assert
            await _skinProfileRepository.Received(1).CreateSkinProfileAsync(dto);
        }
    }

}