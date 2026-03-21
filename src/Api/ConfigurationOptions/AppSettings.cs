using ModularMonolith.BuildingBlocks.EventBus;

namespace ModularMonolith.Api.ConfigurationOptions;

public class AppSettings
{
    public ModuleOptions Modules { get; set; }
    public EventBusOptions EventBus { get; set; }
    public AuthenticationOptions Authentication { get; set; }
    public CORS CORS { get; set; }
}
