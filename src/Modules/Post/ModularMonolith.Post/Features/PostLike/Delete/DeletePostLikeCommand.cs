using BuildingBlocks.Core.CQRS;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
        var post = await _postDbContext.Posts
           .FirstOrDefaultAsync(x => x.Id == request.PostId, cancellationToken);

        if (post is null)
        {
            throw new PostNotFoundException();
        }

        var postLike = await  _postDbContext.PostLikes
            .FirstOrDefaultAsync(pl => pl.PostId == request.PostId && pl.UserId == request.UserId, cancellationToken);

        if (postLike is null)
        {
            return Unit.Value;
        }

        bool UnlikeCheck = postLike.Unlike();

        if (UnlikeCheck)
        {
            post.DecrementLikeCount();
        }

        await _postDbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}