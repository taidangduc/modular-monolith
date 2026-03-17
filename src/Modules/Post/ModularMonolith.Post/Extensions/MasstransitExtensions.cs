using BuildingBlocks.Masstransit;
using MassTransit;

namespace ModularMonolith.Post.Extensions;

public class MasstransitExtensions : IMasstransitModule
{
    public void ConfigureTopology(IBusFactoryConfigurator cfg, IRegistrationContext context)
    {
    }
}
