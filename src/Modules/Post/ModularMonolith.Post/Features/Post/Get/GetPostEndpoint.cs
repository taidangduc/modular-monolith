using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ModularMonolith.Post.Features.Post.Get;

[ApiController]
[Route("api/posts/{postId}")]
public class GetPostEndpoint : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(
        Guid postId,
        IMediator mediator = null!,
        CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new GetPostQuery(postId), cancellationToken);
        if (result is null)
            return NotFound();
        return Ok(result);
    }
}
