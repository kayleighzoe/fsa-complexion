using Complexion.Exceptions;
using Complexion.Models.Skin;
using Complexion.Repository.Skin;
using Complexion.Services.Skin;
using NSubstitute;

namespace Complexion.Tests.Services.Skin
{
    public class SkinProfileServiceTests
    {
        private ISkinProfileRepository _skinProfileRepository;
        private SkinProfileService _skinProfileService;

        [SetUp]
        public void Setup()
        {
            _skinProfileRepository = Substitute.For<ISkinProfileRepository>();

            _skinProfileService = new SkinProfileService(_skinProfileRepository);
        }

        [Test]
        public async Task GIVEN_SkinProfilesExist_WHEN_GettingAllSkinProfilesAsync_THEN_ReturnSkinProfilesFromRepository()
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
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.EqualTo(expectedProfiles));
                _skinProfileRepository.Received(1).GetAllSkinProfilesAsync();
            });
        }

        [Test]
        public async Task GIVEN_SkinProfileExists_WHEN_GettingSkinProfileAsync_THEN_ReturnSkinProfileFromRepository()
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

            _skinProfileRepository.GetSkinProfileAsync(id).Returns(expectedProfile);

            // Act
            var result = await _skinProfileService.GetSkinProfileAsync(id);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.EqualTo(expectedProfile));
                _skinProfileRepository.Received(1).GetSkinProfileAsync(id);
            });
        }

        [Test]
        public void GIVEN_SkinProfileDoesNotExist_WHEN_GettingSkinProfileAsync_THEN_ThrowException()
        {
            // Arrange
            var id = Guid.NewGuid();
            _skinProfileRepository.GetSkinProfileAsync(id).Returns((SkinProfile?)null);

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await _skinProfileService.GetSkinProfileAsync(id));
        }

        [Test]
        public async Task GIVEN_CombinationExists_WHEN_GettingSkinProfileIdAsync_THEN_ReturnIdFromRepository()
        {
            // Arrange
            var expectedId = Guid.NewGuid();
            var skinProfile = new SkinProfile
            {
                SkinProfileId = expectedId,
                ShadeId = 4,
                UndertoneId = 5,
                HasTint = false
            };

            _skinProfileRepository.GetSkinProfileByCombinationAsync(4, 5, false).Returns(skinProfile);

            // Act
            var result = await _skinProfileService.GetSkinProfileIdAsync(4, 5, false);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.EqualTo(expectedId));
                _skinProfileRepository.Received(1).GetSkinProfileByCombinationAsync(4, 5, false);
            });
        }

        [Test]
        public void GIVEN_CombinationDoesNotExist_WHEN_GettingSkinProfileIdAsync_THEN_ThrowNotFoundException()
        {
            // Arrange
            _skinProfileRepository.GetSkinProfileByCombinationAsync(4, 5, false).Returns((SkinProfile?)null);

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await _skinProfileService.GetSkinProfileIdAsync(4, 5, false));
        }
    }
}