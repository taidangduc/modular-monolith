using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ModularMonolith.Post.Features.Post.List;

[ApiController]
[Route("api/posts")]
public class ListPostsEndpoint : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> List(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        IMediator mediator = null!,
        CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new ListPostsQuery(pageNumber, pageSize), cancellationToken);
        return Ok(result);
    }
}
