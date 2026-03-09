using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace User.Features.Preferences.Update;

[ApiController]
[Route("api/user")]
public class UpdatePreferenceEndpoint : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdatePreferenceEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("preferences")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<Guid> Update(
        UpdatePreferenceCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return result;
    }
}
