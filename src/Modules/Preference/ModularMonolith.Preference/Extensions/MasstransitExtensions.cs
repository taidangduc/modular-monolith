using BuildingBlocks.Masstransit;
using MassTransit;
using ModularMonolith.Preference.IntegrationEvents.EventHandlers;

namespace ModularMonolith.Preference.Extensions
{
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

}
