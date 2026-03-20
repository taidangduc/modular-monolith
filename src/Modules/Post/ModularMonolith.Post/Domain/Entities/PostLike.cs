using ModularMonolith.BuildingBlocks.Core.SeedWork;

namespace ModularMonolith.Post.Domain.Entities;

public class PostLike : AuditableEntity<Guid>, ISoftDelete, IAggregateRoot
{
    public Guid PostId { get; set; }
    public Guid UserId { get; set; }
    public bool IsDeleted { get; set; }

    public static PostLike Create(Guid postId, Guid userId)
    {
        var postLike = new PostLike
        {
            PostId = postId,
            UserId = userId,
            CreatedAt = DateTime.UtcNow,
        };

        return postLike;
    }

    public void MarkAsDeleted()
    {
        IsDeleted = true;
        LastModifiedAt = DateTime.UtcNow;
    }

    public void MarkAsActive()
    {
        IsDeleted = false;
        LastModifiedAt = DateTime.UtcNow;
    }
}
