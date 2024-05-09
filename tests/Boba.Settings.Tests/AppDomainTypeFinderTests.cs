using FluentAssertions;

namespace Boba.Settings.Tests;

public class AppDomainTypeFinderTests
{
    [Fact]
    public void FindClassesOfType_WhenTypeNotFound_ShouldReturnEmptyCollection()
    {
        // Arrange
        var typeFinder = new AppDomainTypeFinder();

        // Act
        var result = typeFinder.FindClassesOfType(typeof(IDummyInterface));

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void FindClassesOfType_WhenTypeFound_ShouldReturnMatchingTypes()
    {
        // Arrange
        var typeFinder = new AppDomainTypeFinder();

        // Act
        var result = typeFinder.FindClassesOfType(typeof(ISampleInterface));

        // Assert
        result.Should().Contain(typeof(SampleClass));
    }

    [Fact]
    public void FindClassesOfType_IncludeAbstractClasses_ShouldReturnAbstractClasses()
    {
        // Arrange
        var typeFinder = new AppDomainTypeFinder();

        // Act
        var result = typeFinder.FindClassesOfType(typeof(AbstractSampleClass), onlyConcreteClasses: false);

        // Assert
        result.Should().Contain(typeof(AbstractSampleClass));
    }

    [Fact]
    public void FindClassesOfType_WhenTypeFoundAndOnlyConcreteClassesSet_ShouldNotReturnAbstractClasses()
    {
        // Arrange
        var typeFinder = new AppDomainTypeFinder();

        // Act
        var result = typeFinder.FindClassesOfType(typeof(ISampleInterface)); // SampleClass is a concrete class implementing ISampleInterface

        // Assert
        result.Should().NotContain(typeof(AbstractSampleClass));
    }
}

public interface IDummyInterface { }

public interface ISampleInterface { }

public class SampleClass : ISampleInterface { }

public abstract class AbstractSampleClass : ISampleInterface { }
