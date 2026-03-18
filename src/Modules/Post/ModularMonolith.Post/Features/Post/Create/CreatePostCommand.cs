using Ardalis.GuardClauses;
using ModularMonolith.BuildingBlocks.Core.CQRS;
using ModularMonolith.Post.Infrastructure;

namespace ModularMonolith.Post.Features.Post.Create;

public record CreatePostCommand(Guid AuthorId, string Content) : ICommand<Guid>;

internal class CreatePostCommandHandler : ICommandHandler<CreatePostCommand, Guid>
{
    private readonly PostDbContext _postDbContext;

    public CreatePostCommandHandler(PostDbContext postDbContext)
    {
        _postDbContext = postDbContext;
    }

    public async Task<Guid> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));
        Guard.Against.NullOrWhiteSpace(request.Content, nameof(request.Content));

        var post = Domain.Entities.Post.Create(request.AuthorId, request.Content);

        await _postDbContext.Posts.AddAsync(post, cancellationToken);
        await _postDbContext.SaveChangesAsync(cancellationToken);

        return post.Id;
    }
}
