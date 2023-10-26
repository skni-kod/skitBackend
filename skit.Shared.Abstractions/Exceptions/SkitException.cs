namespace skit.Shared.Abstractions.Exceptions;

public abstract class SkitException : Exception
{
    protected SkitException(string message) : base(message) { }
}