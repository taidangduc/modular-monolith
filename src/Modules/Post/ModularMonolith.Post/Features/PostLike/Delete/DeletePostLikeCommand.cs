using Ardalis.GuardClauses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ModularMonolith.BuildingBlocks.Core.CQRS;
using ModularMonolith.Post.Domain.Exceptions;
using ModularMonolith.Post.Infrastructure;

namespace ModularMonolith.Post.Features.PostLike.Delete;

public record DeletePostLikeCommand(Guid PostId, Guid UserId) : ICommand;

internal class DeletePostLikeCommandHandler : ICommandHandler<DeletePostLikeCommand>
{
    private readonly PostDbContext _postDbContext;

    public DeletePostLikeCommandHandler(PostDbContext postDbContext)
    {
        _postDbContext = postDbContext;
    }

    public async Task<Unit> Handle(DeletePostLikeCommand request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));

        var post = await _postDbContext.Posts
            .FirstOrDefaultAsync(p => p.Id == request.PostId, cancellationToken);

        if (post is null)
        {
            throw new PostNotFoundException();
        }

        var postLike = await _postDbContext.PostLikes
            .FirstOrDefaultAsync(o => 
                o.PostId == request.PostId && 
                o.UserId == request.UserId &&
                o.IsDeleted == false, 
                cancellationToken);

        if(postLike is null)
        {
            return Unit.Value;
        }

        postLike.MarkAsDeleted();
        post.DecreaseLikeCount();

        await _postDbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
