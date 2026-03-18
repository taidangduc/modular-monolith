using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ModularMonolith.Post.Features.PostLike.Create;

[ApiController]
[Route("api/posts/{postId}/like")]
public class LikePostEndpoint : ControllerBase
{
    private readonly IMediator _mediator;

    public LikePostEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Like(
        Guid postId,
        Guid userId,
        CancellationToken cancellationToken)
    {
        var command = new LikePostCommand(postId, userId);
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }
}
