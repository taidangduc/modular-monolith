using Grpc.Core;
using MediatR;
using ModularMonolith.Preference.Features;
using ModularMonolith.Preference.Features.Get;

namespace ModularMonolith.Preference.Grpc.Services;

public class PreferenceService : PreferenceGrpcService.PreferenceGrpcServiceBase
{
    private readonly IMediator _mediator;

    public PreferenceService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task<GetPreferenceResponse> GetPreference(GetPreferenceRequest request, ServerCallContext context)
    {
        var data = await _mediator.Send(new GetPreferenceQuery(new Guid(request.Id)));

        return data is not null ? MapToPreferenceResponse(data) : new();
    }

    private static GetPreferenceResponse MapToPreferenceResponse(PreferenceDTO dto)
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
