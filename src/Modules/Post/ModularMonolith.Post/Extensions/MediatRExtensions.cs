using Microsoft.Extensions.DependencyInjection;

namespace ModularMonolith.Post.Extensions;

public static class MediatRExtensions
{
    public static IServiceCollection AddCustomMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(PostRoot).Assembly));
        return services;
    }
}
