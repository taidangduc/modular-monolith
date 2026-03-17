using ModularMonolith.BuildingBlocks.Exceptions;

namespace ModularMonolith.Identity.Domain.Exceptions;

public class InvalidUserException : ValidationException
{
    public InvalidUserException(string message) : base(message)
    {
    }
}
