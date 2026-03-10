using BuildingBlocks.Masstransit;
using MassTransit;
using ModularMonolith.Profile.IntegrationEvents.EventHandlers;

namespace ModularMonolith.Profile.Extensions;

public class MasstransitExtensions : IMasstransitModule
{
    public void ConfigureTopology(IBusFactoryConfigurator cfg, IRegistrationContext context)
    {
        cfg.ReceiveEndpoint("identity-user", e =>
        {
            e.ConfigureConsumer<UserCreatedEventHandler>(context);
        });
    }
}
