using BuildingBlocks.Contracts;
using MassTransit;
using ModularMonolith.Preference.Grpc.Services;

namespace ModularMonolith.Notification.IntegrationEvents.EventHandlers;

public class UserCreatedEventHandler : IConsumer<UserCreated>
{
    private readonly PreferenceGrpcService.PreferenceGrpcServiceClient _grpcClient;

    public UserCreatedEventHandler(PreferenceGrpcService.PreferenceGrpcServiceClient grpcClient)
    {
        _grpcClient = grpcClient;
    }

    public async Task Consume(ConsumeContext<UserCreated> context)
    {
        if(context.Message == null)
        {
            return;
        }

        var user = context.Message;

        if (string.IsNullOrEmpty(user.Email))
        {
            return;
        }

        var preference = await _grpcClient.GetPreferenceAsync(new GetPreferenceRequest { Id = user.UserId.ToString() });

        if (preference == null)
        {
            return;
        }

        var preferenceDto = preference.Preference
                           .Select(p => new PreferenceDto(
                               (BuildingBlocks.Contracts.ChannelType)p.Channel,
                               p.IsOptOut))
                           .ToList();



        return;
    }
}

