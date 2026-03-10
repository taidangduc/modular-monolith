using BuildingBlocks.Exception;

namespace ModularMonolith.Identity.Domain.Exceptions;

public class InvalidUserException : AppException
{
    public InvalidUserException(string message) : base(message)
    {
    }
}
