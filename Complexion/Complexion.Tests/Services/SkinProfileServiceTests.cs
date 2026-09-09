using Complexion.Models.Dbo;
using Complexion.Repository.Dbo;
using Complexion.Services;
using FluentValidation;
using FluentValidation.Results;
using NSubstitute;

namespace Complexion.Tests.Services
{
    public class SkinProfileServiceTests
    {
        private ISkinProfileRepository _skinProfileRepository;
        private SkinProfileService _skinProfileService;
        private IValidator<CreateSkinProfileDto> _createValidator;

        [SetUp]
        public void Setup()
        {
            _skinProfileRepository = Substitute.For<ISkinProfileRepository>();
            _createValidator = Substitute.For<IValidator<CreateSkinProfileDto>>();

            _skinProfileService  = new SkinProfileService(_skinProfileRepository, _createValidator);

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
            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _skinProfileService.GetSkinProfileAsync(id));
        }

        [Test]
        public async Task GIVEN_ValidDto_WHEN_CreatingSkinProfileAsync_THEN_CallRepositoryCreate()
        {
            // Arrange
            var dto = new CreateSkinProfileDto
            {
                ShadeId = 4,
                UndertoneId = 5,
                HasTint = false
            };

            _createValidator.ValidateAsync(Arg.Any<CreateSkinProfileDto>(), Arg.Any<CancellationToken>()).Returns(new ValidationResult());

            // Act
            await _skinProfileService.CreateSkinProfileAsync(dto);

            // Assert
            await _skinProfileRepository.Received(1).CreateSkinProfileAsync(dto);
        }
    }

}