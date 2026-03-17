using Ardalis.GuardClauses;
using BuildingBlocks.Core.CQRS;
using Microsoft.EntityFrameworkCore;
using ModularMonolith.Post.Infrastructure;

namespace ModularMonolith.Post.Features.Post.List;

public record ListPostsQuery(int PageNumber = 1, int PageSize = 10) : IQuery<List<PostDto>>;

internal class ListPostsQueryHandler : IQueryHandler<ListPostsQuery, List<PostDto>>
{
    private readonly PostDbContext _postDbContext;

    public ListPostsQueryHandler(PostDbContext postDbContext)
    {
        _postDbContext = postDbContext;
    }

    public async Task<List<PostDto>> Handle(ListPostsQuery query, CancellationToken cancellationToken)
    {
        Guard.Against.Null(query, nameof(query));

        var queryable = _postDbContext.Posts.AsQueryable();

        queryable = queryable
            .Where(x => !x.IsDeleted)
            .OrderByDescending(x => x.CreatedAt)
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize);

        var posts = await queryable
            .Select(x => new PostDto(
                x.Id, 
                x.AuthorId,
                x.Content,
                x.LikeCount,
                x.CreatedAt ?? DateTime.UtcNow))
            .ToListAsync(cancellationToken);

        return posts;
    }
}
