using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using ModularMonolith.BuildingBlocks.Core.CQRS;
using ModularMonolith.Post.Domain.Exceptions;
using ModularMonolith.Post.Infrastructure;

namespace ModularMonolith.Post.Features.PostLike.Create;

public record LikePostCommand(Guid PostId, Guid UserId) : ICommand<Guid>;

internal class LikePostCommandHandler : ICommandHandler<LikePostCommand, Guid>
{
    private readonly PostDbContext _postDbContext;

    public LikePostCommandHandler(PostDbContext postDbContext)
    {
        _postDbContext = postDbContext;
    }

    public async Task<Guid> Handle(LikePostCommand request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));

        var post = await _postDbContext.Posts
            .FirstOrDefaultAsync(x => x.Id == request.PostId, cancellationToken);

        if (post is null)
        {
            throw new PostNotFoundException();
        }

        var postLike = await _postDbContext.PostLikes
            .FirstOrDefaultAsync(x => x.PostId == request.PostId && x.UserId == request.UserId, cancellationToken);

        if (postLike is not null && postLike.IsDeleted == false)
        {
            return post.Id;
        }

        if (postLike is not null && postLike.IsDeleted)
        {
            postLike.MarkAsActive();
        }

        else
        {
            postLike = Domain.Entities.PostLike.Create(request.PostId, request.UserId);
            await _postDbContext.PostLikes.AddAsync(postLike, cancellationToken);
        }

        post.IncreaseLikeCount();

        await _postDbContext.SaveChangesAsync(cancellationToken);

        return post.Id;
    }
}
