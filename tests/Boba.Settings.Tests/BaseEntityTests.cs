using FluentAssertions;

namespace Boba.Settings.Tests;

public class BaseEntityTests
{
    [Fact]
    public void BaseEntity_Id_ShouldBeSettableAndGettable()
    {
        // Arrange
        var entity = new TestEntity();
        var expectedId = 123;

        // Act
        entity.Id = expectedId;
        var actualId = entity.Id;

        // Assert
        actualId.Should().Be(expectedId);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(-1)]
    [InlineData(-100)]
    [InlineData(int.MinValue)]
    public void BaseEntity_SetIdValue_ShouldNotThrowException(int value)
    {
        // Arrange
        var entity = new TestEntity();

        // Act
        var exceptionThrown = false;
        try
        {
            entity.Id = value;
        }
        catch
        {
            exceptionThrown = true;
        }

        // Assert
        exceptionThrown.Should().BeFalse();
    }
}

public class TestEntity : BaseEntity
{
    // Additional properties or methods can be added for testing purposes
}