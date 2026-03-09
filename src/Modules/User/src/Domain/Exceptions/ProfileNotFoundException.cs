using BuildingBlocks.Exception;

namespace User.Domain.Exceptions;

public class ProfileNotFoundException : AppException
{
    public ProfileNotFoundException() : base($"Not found profile")
    {
    }
}
