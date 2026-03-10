using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ModularMonolith.Profile.Features.Update;

[ApiController]
[Route("api/user")]
public class UpdateProfileEndpoint : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateProfileEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("profile")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<Guid> UpdateProfile(UpdateProfileCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        return result;
    }
}
