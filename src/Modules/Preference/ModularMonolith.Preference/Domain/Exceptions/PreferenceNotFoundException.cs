using ModularMonolith.BuildingBlocks.Exceptions;

namespace ModularMonolith.Preference.Domain.Exceptions;

public class PreferenceNotFoundException : NotFoundException
{
    public PreferenceNotFoundException() : base("Not found preference")
    {
    }
}
