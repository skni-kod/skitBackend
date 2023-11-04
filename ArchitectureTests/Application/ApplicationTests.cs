using skit.Application;
using System.Reflection;

namespace ArchitectureTests.Application;
public class ApplicationTests
{
    private const string CoreNamespace = "skit.Core";

    private static readonly Assembly ApplicationAssembly = typeof(AssemblyReference).Assembly;

    [Fact]
    public void Handlers_Should_HaveDependencyOnCore()
    {
        // Arrange

        // Act
        var result = Types
            .InAssembly(ApplicationAssembly)
            .That()
            .HaveNameEndingWith("Handler")
            .Should()
            .HaveDependencyOn(CoreNamespace)
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }
}