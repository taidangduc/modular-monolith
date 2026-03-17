using Ardalis.GuardClauses;
using BuildingBlocks.Core.CQRS;
using Microsoft.EntityFrameworkCore;
using ModularMonolith.Post.Domain.Exceptions;
using ModularMonolith.Post.Infrastructure;

namespace ModularMonolith.Post.Features.PostLike.Create;

public record CreatePostLikeCommand(Guid PostId, Guid UserId) : ICommand<Guid>;

internal class CreatePostLikeCommandHandler : ICommandHandler<CreatePostLikeCommand, Guid>
{
    private readonly PostDbContext _postDbContext;

    public CreatePostLikeCommandHandler(PostDbContext postDbContext)
    {
        _postDbContext = postDbContext;
    }

    public async Task<Guid> Handle(CreatePostLikeCommand request, CancellationToken cancellationToken)
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

        if (postLike is null)
        {
            postLike = Domain.Entities.PostLike.Create(request.PostId, request.UserId);
            await _postDbContext.PostLikes.AddAsync(postLike, cancellationToken);

            post.IncrementLikeCount();
        }

        else
        {
            bool likeCheck = postLike.Like();

            if (likeCheck)
            {
                post.IncrementLikeCount();
            }
        }

        await _postDbContext.SaveChangesAsync(cancellationToken);

        return postLike.Id;
    }
}
