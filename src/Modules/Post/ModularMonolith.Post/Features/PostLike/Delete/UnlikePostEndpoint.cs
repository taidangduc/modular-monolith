using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ModularMonolith.Post.Features.PostLike.Delete;

[ApiController]
[Route("api/posts/{postId}/like")]
public class UnlikePostEndpoint : ControllerBase
{
    private readonly IMediator _mediator;

    public UnlikePostEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Unlike(
        Guid postId,
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        await _mediator.Send(new UnlikePostCommand(postId, userId), cancellationToken);
        return NoContent();
    }
}
