using skit.Shared.Abstractions.Exceptions;

namespace skit.Core.Technologies.Exceptions;

public class TechnologyNotFoundException : SkitException
{
    public TechnologyNotFoundException() : base("Technology does not exist")
    {
        
    }
}