using ModularMonolith.Notification.IntegrationEvents.Events;

namespace ModularMonolith.Notification.Infrastructure.Projections;

public class ProfileView
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
}

public sealed class ProfileViewProjection
{
    public static ProfileView Apply(ProfileView view, ProfileUpdatedIntegrationEvent @event)
    {
        view.FullName = @event.FullName;
        view.Email = @event.Email;

        return view;
    }

    public static ProfileView Create(ProfileView view, ProfileCreatedIntegrationEvent @event)
    {
        view.UserId = @event.UserId;
        view.FullName = @event.FullName;
        view.Email = @event.Email;
        return view;
    }
}