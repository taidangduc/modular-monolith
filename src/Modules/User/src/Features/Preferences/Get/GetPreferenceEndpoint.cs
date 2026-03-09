using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace User.Features.Preferences.Get;

[ApiController]
[Route("api/user")]
public class GetPreferenceEndpoint : ControllerBase
{
    [HttpGet("preferences/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPreference(Guid id, IMediator mediator, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetPreferenceQuery(id), cancellationToken);
        return Ok(result);
    }
}