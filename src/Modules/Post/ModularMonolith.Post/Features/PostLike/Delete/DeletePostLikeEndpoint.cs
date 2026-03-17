using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ModularMonolith.Post.Features.PostLike.Delete;

[ApiController]
[Route("api/posts/{postId}/likes")]
public class DeletePostLikeEndpoint : ControllerBase
{
    private readonly IMediator _mediator;

    public DeletePostLikeEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(
        Guid postId,
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        await _mediator.Send(new DeletePostLikeCommand(postId, userId), cancellationToken);
        return NoContent();
    }

}
