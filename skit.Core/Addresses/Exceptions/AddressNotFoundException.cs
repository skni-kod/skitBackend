using skit.Shared.Abstractions.Exceptions;

namespace skit.Core.Addresses.Exceptions;

public sealed class AddressNotFoundException : SkitException
{
    public AddressNotFoundException() : base("Address not found") { }
}