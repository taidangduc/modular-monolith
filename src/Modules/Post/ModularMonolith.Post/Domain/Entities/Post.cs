using ModularMonolith.BuildingBlocks.Core.SeedWork;

namespace ModularMonolith.Post.Domain.Entities;

public class Post : AuditableEntity<Guid>, ISoftDelete, IAggregate
{
    public Guid AuthorId { get; set; }
    public string Content { get; set; }
    public int LikeCount { get; set; }
    public bool IsDeleted { get; set; }

    public static Post Create(Guid authorId, string content)
    {
        var post = new Post
        {
            Id = Guid.NewGuid(),
            AuthorId = authorId,
            Content = content,
            CreatedAt = DateTime.UtcNow,
        };

        return post;
    }

    public void IncreaseLikeCount()
    {
        LikeCount++;
        LastModifiedAt = DateTime.UtcNow;
    }

    public void DecreaseLikeCount()
    {
        if (LikeCount <= 0)
        {
            return;
        }

        LikeCount--;
        LastModifiedAt = DateTime.UtcNow;
    }
}
