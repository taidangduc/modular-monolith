using BuildingBlocks.Exception;

namespace ModularMonolith.Preference.Domain.Exceptions;

public class PreferenceNotFoundException : DomainException
{
    public PreferenceNotFoundException() : base("Not found preference")
    {
    }
}
