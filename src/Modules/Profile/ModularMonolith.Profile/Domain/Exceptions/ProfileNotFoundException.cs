using ModularMonolith.BuildingBlocks.Exceptions;

namespace ModularMonolith.Profile.Domain.Exceptions;

public class ProfileNotFoundException : NotFoundException
{
    public ProfileNotFoundException() : base($"Not found profile")
    {
    }
}
