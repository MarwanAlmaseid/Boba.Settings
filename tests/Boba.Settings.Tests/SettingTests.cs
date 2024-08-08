using FluentAssertions;

namespace Boba.Settings.Tests;

public class SettingTests
{
    [Fact]
    public void Setting_Name_ShouldBeSettableAndGettable()
    {
        // Arrange
        var expectedName = "SettingName";

        // Act
        var setting = new BobaSetting { Name = expectedName };

        // Assert
        setting.Name.Should().Be(expectedName);
    }

    [Fact]
    public void Setting_Value_ShouldBeSettableAndGettable()
    {
        // Arrange
        var expectedValue = "SettingValue";
        var setting = new BobaSetting { Name = "SettingName" };

        // Act
        setting.Value = expectedValue;
        var actualValue = setting.Value;

        // Assert
        actualValue.Should().Be(expectedValue);
    }

    [Fact]
    public void Setting_Value_DefaultValueShouldBeNull()
    {
        // Arrange
        var setting = new BobaSetting { Name = "SettingName" };

        // Act
        var value = setting.Value;

        // Assert
        value.Should().BeNull();
    }
}
