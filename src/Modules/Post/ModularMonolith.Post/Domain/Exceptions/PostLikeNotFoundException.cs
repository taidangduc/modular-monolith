using BuildingBlocks.Exception;

namespace ModularMonolith.Post.Domain.Exceptions;

public class PostLikeNotFoundException : DomainException
{
    public PostLikeNotFoundException() : base("Post like not found")
    {
    }
}
