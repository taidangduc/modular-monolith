using ModularMonolith.BuildingBlocks.Exceptions;

namespace ModularMonolith.Post.Domain.Exceptions;

public class PostLikeNotFoundException : NotFoundException
{
    public PostLikeNotFoundException() : base("Post like not found")
    {
    }
}
