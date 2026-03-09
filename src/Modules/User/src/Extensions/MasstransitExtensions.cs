using BuildingBlocks.Masstransit;
using MassTransit;
using User.IntegrationEvents.EventHandlers;

namespace User.Extensions;

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
