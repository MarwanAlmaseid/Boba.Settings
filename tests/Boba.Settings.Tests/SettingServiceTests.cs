using FluentAssertions;
using Moq;

namespace Boba.Settings.Tests;

public class SettingServiceTests
{
    private Mock<ISettingRepository> _mockSettingRepository;

    public SettingServiceTests()
    {
        _mockSettingRepository = new Mock<ISettingRepository>();
    }

    #region GetAllSettingsAsync

    [Fact]
    public async Task GetAllSettingsAsync_Should_ReturnSettingsFromRepository()
    {
        // Arrange
        var expectedSettings = GetDumySettings();
        _mockSettingRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(expectedSettings);

        var _settingService = new SettingService(_mockSettingRepository.Object);

        // Act
        var actualSettings = await _settingService.GetAllSettingsAsync();

        // Assert
        actualSettings.Should().BeEquivalentTo(expectedSettings);
    }

    [Fact]
    public async Task GetAllSettingsAsync_ShouldReturnEmptyList_WhenRepositoryIsEmpty()
    {
        // Arrange
        _mockSettingRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Setting>());

        var settingService = new SettingService(_mockSettingRepository.Object);

        // Act
        var result = await settingService.GetAllSettingsAsync();

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllSettingsAsync_ShouldReturnEmptyList_WhenRepositoryIsNull()
    {
        // Arrange
        _mockSettingRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync((List<Setting>)null);

        var settingService = new SettingService(_mockSettingRepository.Object);

        // Act
        var result = await settingService.GetAllSettingsAsync();

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllSettingsAsync_ShouldHandleException_WhenRepositoryThrowsException()
    {
        // Arrange
        _mockSettingRepository.Setup(repo => repo.GetAllAsync()).ThrowsAsync(new Exception("Repository exception"));

        var settingService = new SettingService(_mockSettingRepository.Object);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => settingService.GetAllSettingsAsync());
    }

    #endregion


    #region GetSettingByIdAsync

    [Fact]
    public async Task GetSettingByIdAsync_ShouldReturnSetting_WhenIdExists()
    {
        // Arrange
        int settingId = 1;
        var expectedSetting = new Setting { Id = settingId, Name = "Setting 1", Value = "Value 1" };

        _mockSettingRepository.Setup(repo => repo.GetByIdAsync(settingId)).ReturnsAsync(expectedSetting);

        var settingService = new SettingService(_mockSettingRepository.Object);

        // Act
        var result = await settingService.GetSettingByIdAsync(settingId);

        // Assert
        result.Should().BeEquivalentTo(expectedSetting);
    }

    [Fact]
    public async Task GetSettingByIdAsync_ShouldReturnNull_WhenIdDoesNotExist()
    {
        // Arrange
        int settingId = 1;

        _mockSettingRepository.Setup(repo => repo.GetByIdAsync(settingId)).ReturnsAsync((Setting)null);

        var settingService = new SettingService(_mockSettingRepository.Object);

        // Act
        var result = await settingService.GetSettingByIdAsync(settingId);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetSettingByIdAsync_ShouldHandleException_WhenRepositoryThrowsException()
    {
        // Arrange
        int settingId = 1;

        _mockSettingRepository.Setup(repo => repo.GetByIdAsync(settingId)).ThrowsAsync(new Exception("Repository exception"));

        var settingService = new SettingService(_mockSettingRepository.Object);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => settingService.GetSettingByIdAsync(settingId));
    }

    #endregion


    #region GetSettingByKeyAsync

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public async Task GetSettingByKeyAsync_ShouldReturnDefaultValue_WhenKeyIsNullOrEmpty(string key)
    {
        // Arrange
        var defaultValue = "Default";

        var settingService = new SettingService(Mock.Of<ISettingRepository>());

        // Act
        var result = await settingService.GetSettingByKeyAsync<string>(key, defaultValue);

        // Assert
        result.Should().Be(defaultValue);
    }

    #endregion


    public List<Setting> GetDumySettings()
    {
        return new List<Setting>
        {
            new Setting { Id=1, Name="Name 1", Value= "Value 1" } ,
            new Setting { Id=2, Name="Name 2", Value= "Value 2" },
            new Setting { Id=3, Name="Name 3", Value= "Value 3" }
        };
    }
}
