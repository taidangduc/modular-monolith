using Microsoft.Extensions.DependencyInjection;

namespace ModularMonolith.Profile.Extensions;

public static class MediatRExtensions
{
    public static IServiceCollection AddCustomMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ProfileRoot).Assembly));
        return services;
    }
}
