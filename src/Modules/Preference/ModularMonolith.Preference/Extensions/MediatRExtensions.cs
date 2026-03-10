using Microsoft.Extensions.DependencyInjection;

namespace ModularMonolith.Preference.Extensions;

public static class MediatRExtensions
{
    public static IServiceCollection AddCustomMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(PreferenceRoot).Assembly));
        return services;
    }
}
