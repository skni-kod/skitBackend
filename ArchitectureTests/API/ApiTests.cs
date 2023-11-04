using skit.API;
using System.Reflection;

namespace ArchitectureTests.API;
public class ApiTests
{
    private static readonly Assembly ApiAssembly = typeof(AssemblyReference).Assembly;

    [Fact]
    public void Controllers_Should_HaveDependencyOnMediatR()
    {
        // Arrange

        // Act
        var result = Types
            .InAssembly(ApiAssembly)
            .That()
            .HaveNameEndingWith("Controller")
            .Should()
            .HaveDependencyOn("MediatR")
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }
}