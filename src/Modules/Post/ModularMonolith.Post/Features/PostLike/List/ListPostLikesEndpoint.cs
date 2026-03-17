using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ModularMonolith.Post.Features.PostLike.List;

[ApiController]
[Route("api/posts/{postId}/likes")]
public class ListPostLikesEndpoint : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> List(
        Guid postId,
        IMediator mediator = null!,
        CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new ListPostLikesQuery(postId), cancellationToken);
        return Ok(result);
    }
}
