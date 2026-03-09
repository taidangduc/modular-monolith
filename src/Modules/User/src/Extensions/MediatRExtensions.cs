using Microsoft.Extensions.DependencyInjection;

namespace User.Extensions;
public static class MediatRExtensions
{
    public static IServiceCollection AddCustomMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(UserRoot).Assembly));
        return services;
    }
}
