using skit.Shared.Abstractions.Exceptions;

namespace skit.Core.Offers.Exceptions;
public class OfferForbiddenException : SkitException
{
    public OfferForbiddenException() : base("Offer does not exist in your company")
    {
    }
}