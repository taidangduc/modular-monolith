using BuildingBlocks.Exception;

namespace ModularMonolith.Post.Domain.Exceptions;

public class PostNotFoundException : DomainException
{
    public PostNotFoundException() : base("Post not found")
    {
    }
}
