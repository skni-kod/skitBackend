using skit.Shared.Abstractions.Exceptions;

namespace skit.Core.Files.Exceptions;

public sealed class FileNotFundException : SkitException
{
    public FileNotFundException() : base("Fil not found") { }
}