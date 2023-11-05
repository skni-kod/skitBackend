using System.Reflection;

namespace ArchitectureTests;

public class ArchitectureTests
{
    private const string ApplicationNamespace = "skit.Application";
    private const string InfrastructureNamespace = "skit.Infrastructure";
    private const string ApiNamespace = "skit.API";

    private static readonly Assembly CoreAssembly = typeof(skit.Core.AssemblyReference).Assembly;
    private static readonly Assembly ApplicationAssembly = typeof(skit.Application.AssemblyReference).Assembly;
    private static readonly Assembly InfrastructureAssembly = typeof(skit.Infrastructure.AssemblyReference).Assembly;

    [Fact]
    public void Core_Should_Not_HaveDependencyOnOtherProjects()
    {
        // Arrange
        var otherProjects = new[]
        {
            ApplicationNamespace,
            InfrastructureNamespace,
            ApiNamespace
        };

        // Act
        var result = Types
            .InAssembly(CoreAssembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherProjects)
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Application_Should_Not_HaveDependencyOnOtherProjects()
    {
        // Arrange
        var otherProjects = new[]
        {
            InfrastructureNamespace,
            ApiNamespace
        };

        // Act
        var result = Types
            .InAssembly(ApplicationAssembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherProjects)
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Infrastructure_Should_Not_HaveDependencyOnApiProject()
    {
        // Arrange
        var otherProjects = new[]
        {
            ApiNamespace
        };

        // Act
        var result = Types
            .InAssembly(InfrastructureAssembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherProjects)
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }
}