using ModularMonolith.BuildingBlocks.Exceptions;

namespace ModularMonolith.Identity.Domain.Exceptions;

public class InvalidUserRoleException : ValidationException
{
    public InvalidUserRoleException(string message) : base(message)
    {
    }
}
