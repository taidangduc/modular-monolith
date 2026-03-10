using Microsoft.Extensions.DependencyInjection;

namespace ModularMonolith.Notification.Extensions;

public static class MediatRExtensions
{
    public static IServiceCollection AddCustomMediatR(this IServiceCollection services)
    {
        services.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(NotificationRoot).Assembly));
        return services;
    }
}
