using BuildingBlocks.Core.Model;

namespace ModularMonolith.Post.Domain.Entities;

public record Post : Aggregate<Guid>
{
    public Guid AuthorId { get; private set; }
    public string Content { get; private set; } = default!;
    public int LikeCount { get; private set; }

    public static Post Create(Guid authorId, string content)
    {
        var post = new Post
        {
            Id = Guid.NewGuid(),
            AuthorId = authorId,
            Content = content,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        return post;
    }

    public void IncrementLikeCount()
    {
        LikeCount++;
        UpdatedAt = DateTime.UtcNow;
    }

    public void DecrementLikeCount()
    {
        if (LikeCount > 0)
        {
            LikeCount--;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
