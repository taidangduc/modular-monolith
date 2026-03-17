using ModularMonolith.BuildingBlocks.Exceptions;

namespace ModularMonolith.Post.Domain.Exceptions;

public class PostNotFoundException : NotFoundException
{
    public PostNotFoundException() : base("Post not found")
    {
    }
}
