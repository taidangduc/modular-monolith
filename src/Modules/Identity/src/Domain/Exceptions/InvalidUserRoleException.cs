using BuildingBlocks.Exception;

namespace Identity.Domain.Exceptions;

public class InvalidUserRoleException : AppException
{
    public InvalidUserRoleException(string message) : base(message)
    {
    }
}
