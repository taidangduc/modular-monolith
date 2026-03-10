using Microsoft.Extensions.DependencyInjection;

namespace ModularMonolith.Identity.Extensions;

public static class MediatRExtensions
{
    public static IServiceCollection AddMediatRCustom(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(IdentityRoot).Assembly));  
        return services;
    }
}
