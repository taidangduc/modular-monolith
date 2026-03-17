using Ardalis.GuardClauses;
using BuildingBlocks.Core.CQRS;
using Microsoft.EntityFrameworkCore;
using ModularMonolith.Post.Infrastructure;

namespace ModularMonolith.Post.Features.PostLike.List;

public record ListPostLikesQuery(Guid PostId) : IQuery<List<PostLikeDto>>;

internal class ListPostLikesQueryHandler : IQueryHandler<ListPostLikesQuery, List<PostLikeDto>>
{
    private readonly PostDbContext _postDbContext;

    public ListPostLikesQueryHandler(PostDbContext postDbContext)
    {
        _postDbContext = postDbContext;
    }

    public async Task<List<PostLikeDto>> Handle(ListPostLikesQuery query, CancellationToken cancellationToken)
    {
        Guard.Against.Null(query, nameof(query));

        var postLikes = await _postDbContext.PostLikes
            .Where(x => x.PostId == query.PostId && !x.IsDeleted)
            .OrderByDescending(x => x.CreatedAt)
            .Select(pl => new PostLikeDto(
                pl.Id,
                pl.PostId,
                pl.UserId,
                pl.CreatedAt ?? DateTime.UtcNow
            ))
            .ToListAsync(cancellationToken);

        return postLikes;
    }
}
