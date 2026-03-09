using BuildingBlocks.Exception;

namespace Identity.Domain.Exceptions;

internal class InvalidUserException : AppException
{
    public InvalidUserException(string message) : base(message)
    {
    }
}
