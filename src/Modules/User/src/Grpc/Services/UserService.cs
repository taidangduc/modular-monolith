using Grpc.Core;
using MediatR;
using User.Features.Preferences;
using User.Features.Preferences.Get;

namespace User.Grpc.Services;
public class UserService : UserGrpcService.UserGrpcServiceBase
{
    private readonly IMediator _mediator;

    public UserService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task<GetPreferenceResponse> GetPreference(GetPreferenceRequest request, ServerCallContext context)
    {
        var data = await _mediator.Send(new GetPreferenceQuery(new Guid(request.Id)));

        return data is not null ? MapToPreferenceResponse(data) : new();
    }

    private static GetPreferenceResponse MapToPreferenceResponse(PreferenceDto dto)
    {
        var response = new GetPreferenceResponse { UserId = dto.UserId.ToString() };

        foreach (var item in dto.Preferences)
        {
            response.Preference.Add(new ChannelPreference
            {
                Channel = (ChannelType)item.Channel,
                IsOptOut = item.IsOptOut
            });
        }

        return response;
    }
}
