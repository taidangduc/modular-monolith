namespace ModularMonolith.Post.Features;

public record PostDto(Guid Id, Guid AuthorId, string Content, int LikeCount, DateTime CreatedAt);

public record PostLikeDto(Guid Id, Guid PostId, Guid UserId, DateTime CreatedAt);
