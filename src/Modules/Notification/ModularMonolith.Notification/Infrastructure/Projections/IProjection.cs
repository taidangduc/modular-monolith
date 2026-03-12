namespace ModularMonolith.Notification.Infrastructure.Projections;

public interface IProjection<in TEvent>
{
    Task ProjectAsync(TEvent @event);
}
