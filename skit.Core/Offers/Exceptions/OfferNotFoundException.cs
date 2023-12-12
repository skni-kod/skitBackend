using skit.Shared.Abstractions.Exceptions;

namespace skit.Core.Offers.Exceptions;

public class OfferNotFoundException : SkitException
{
    public OfferNotFoundException() : base("Offer does not exist")
    {
    }
}