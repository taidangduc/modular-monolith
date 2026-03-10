using BuildingBlocks.Exception;

namespace ModularMonolith.Profile.Domain.Exceptions;

public class ProfileNotFoundException : AppException
{
    public ProfileNotFoundException() : base($"Not found profile")
    {
    }
}
