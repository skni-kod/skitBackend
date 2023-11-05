using skit.Core;
using skit.Shared.Abstractions.Entities;
using System.Reflection;

namespace ArchitectureTests.Core;
public class CoreTests
{
    private static readonly Assembly CoreAssembly = typeof(AssemblyReference).Assembly;

    [Fact]
    public void Entities_Should_BeSealed()
    {
        // Arrange

        // Act
        var result = Types
            .InAssembly(CoreAssembly)
            .That()
            .Inherit(typeof(Entity))
            .Should()
            .BeSealed()
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }
}