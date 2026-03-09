using BuildingBlocks.Exception;

namespace User.Domain.Exceptions;

public class PreferenceNotFoundException : DomainException
{
    public PreferenceNotFoundException() : base("Not found preference")
    {
    }
}
