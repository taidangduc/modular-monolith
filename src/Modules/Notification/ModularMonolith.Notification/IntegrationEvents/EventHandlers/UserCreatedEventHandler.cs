using ModularMonolith.Preference.Grpc.Services;
using MassTransit;
using ModularMonolith.Notification.IntegrationEvents.Events;

namespace ModularMonolith.Notification.IntegrationEvents.EventHandlers;

public class UserCreatedEventHandler : IConsumer<UserCreatedIntegrationEvent>
{
    private readonly PreferenceGrpcService.PreferenceGrpcServiceClient _grpcClient;

    public UserCreatedEventHandler(PreferenceGrpcService.PreferenceGrpcServiceClient grpcClient)
    {
        _grpcClient = grpcClient;
    }

    public async Task Consume(ConsumeContext<UserCreatedIntegrationEvent> context)
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

        //var preferenceDto = preference.Preference
        //                   .Select(p => new BuildingBlocks.Contracts.Preference.DTOs.ChannelPreference(
        //                       (BuildingBlocks.Contracts.Preference.DTOs.ChannelType)p.Channel,
        //                       p.IsOptOut))
        //                   .ToList();
        return;
    }
}

public class UserCreatedIntegrationEventConsumerDefinition : ConsumerDefinition<UserCreatedEventHandler>
{
    public UserCreatedIntegrationEventConsumerDefinition()
    { 
        Endpoint(x => x.Name = "user-created");
        ConcurrentMessageLimit = 1;
    }
}

