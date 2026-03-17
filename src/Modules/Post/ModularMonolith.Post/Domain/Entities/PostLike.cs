using BuildingBlocks.Core.Model;

namespace ModularMonolith.Post.Domain.Entities;

public record PostLike : Entity<Guid>
{
    public Guid PostId { get; private set; }
    public Guid UserId { get; private set; }

    public static PostLike Create(Guid postId, Guid userId)
    {
        var postLike = new PostLike
        {
            Id = Guid.NewGuid(),
            PostId = postId,
            UserId = userId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        return postLike;
    }

    public bool Like()
    {
        if (IsDeleted)
        {
            IsDeleted = false;
            UpdatedAt = DateTime.UtcNow;
            return true;
        }
        return false;
    }

    public bool Unlike()
    {
        if (!IsDeleted)
        {
            IsDeleted = true;
            UpdatedAt = DateTime.UtcNow;
            return true;
        }
        return false;
    }
}
