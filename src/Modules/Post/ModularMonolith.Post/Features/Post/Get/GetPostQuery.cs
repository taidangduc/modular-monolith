using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using ModularMonolith.BuildingBlocks.Core.CQRS;
using ModularMonolith.Post.Domain.Exceptions;
using ModularMonolith.Post.Infrastructure;

namespace ModularMonolith.Post.Features.Post.Get;

public record GetPostQuery(Guid PostId) : IQuery<PostDto>;

internal class GetPostQueryHandler : IQueryHandler<GetPostQuery, PostDto>
{
    private readonly PostDbContext _postDbContext;

    public GetPostQueryHandler(PostDbContext postDbContext)
    {
        _postDbContext = postDbContext;
    }

    public async Task<PostDto> Handle(GetPostQuery query, CancellationToken cancellationToken)
    {
        Guard.Against.Null(query, nameof(query));

        var post = await _postDbContext.Posts
            .Where(x => x.Id == query.PostId && !x.IsDeleted)
            .Select(x => new PostDto(
                x.Id,
                x.AuthorId,
                x.Content,
                x.LikeCount,
                x.CreatedAt))
            .FirstOrDefaultAsync(cancellationToken);

        if (post is null)
        {
            throw new PostNotFoundException();
        }

        return post;
    }
}
