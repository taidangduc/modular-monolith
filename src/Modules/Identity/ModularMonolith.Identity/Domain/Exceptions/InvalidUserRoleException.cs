using BuildingBlocks.Exception;

namespace ModularMonolith.Identity.Domain.Exceptions;

public class InvalidUserRoleException : AppException
{
    public InvalidUserRoleException(string message) : base(message)
    {
    }
}
