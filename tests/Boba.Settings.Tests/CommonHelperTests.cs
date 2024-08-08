using FluentAssertions;

namespace Boba.Settings.Tests;

public class CommonHelperTests
{
    [Fact]
    public void To_ConvertIntegerToString_ShouldReturnString()
    {
        // Arrange
        int value = 123;

        // Act
        string result = CommonHelper.To<string>(value);

        // Assert
        result.Should().Be("123");
    }

    [Fact]
    public void To_ConvertStringToInteger_ShouldReturnInteger()
    {
        // Arrange
        string value = "456";

        // Act
        int result = CommonHelper.To<int>(value);

        // Assert
        result.Should().Be(456);
    }

    [Fact]
    public void To_ConvertStringToDateTime_ShouldReturnDateTime()
    {
        // Arrange
        string value = "2024-04-07";

        // Act
        DateTime result = CommonHelper.To<DateTime>(value);

        // Assert
        result.Should().Be(new DateTime(2024, 04, 07));
    }

    [Fact]
    public void To_ConvertEnumValueToEnumType_ShouldReturnEnum()
    {
        // Arrange
        int value = 1;

        // Act
        TestEnum result = CommonHelper.To<TestEnum>(value);

        // Assert
        result.Should().Be(TestEnum.Value1);
    }

    [Fact]
    public void To_ConvertEnumTypeToEnumType_ShouldReturnEnum()
    {
        // Arrange
        TestEnum value = TestEnum.Value2;

        // Act
        TestEnum result = CommonHelper.To<TestEnum>(value);

        // Assert
        result.Should().Be(TestEnum.Value2);
    }

    [Fact]
    public void To_ConvertNullValue_ShouldReturnNull()
    {
        // Arrange
        object value = null;

        // Act
        var result = CommonHelper.To<string>(value);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public void To_InvalidConversion_ThrowsInvalidCastException()
    {
        // Arrange
        object value = new(); // Invalid conversion from object to int

        // Act
        Action act = () => CommonHelper.To<int>(value);

        // Assert
        act.Should().Throw<InvalidCastException>();
    }
}

public enum TestEnum
{
    Value1 = 1,
    Value2 = 2
}
