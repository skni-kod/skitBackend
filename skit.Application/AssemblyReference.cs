using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("skit.UnitTests")]

namespace skit.Application
{
    public static class AssemblyReference
    {
        public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
    }
}