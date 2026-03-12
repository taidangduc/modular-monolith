using Microsoft.Extensions.DependencyInjection;

namespace ModularMonolith.Notification.Infrastructure.Projections;

public class ProjectionDispatcher
{
    private readonly IServiceProvider _services;

    public ProjectionDispatcher(IServiceProvider services)
    {
        _services = services;
    }

    public async Task DispatchAsync<TEvent>(TEvent @event)
    {
        var projections = _services.GetServices<IProjection<TEvent>>();

        foreach (var projection in projections)
        {
            await projection.ProjectAsync(@event);
        }
    }
}
