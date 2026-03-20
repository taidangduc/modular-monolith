using ModularMonolith.Contracts.Preference.DTOs;
using ModularMonolith.Notification.IntegrationEvents.Events;

namespace ModularMonolith.Notification.Infrastructure.Projections;

public class PreferenceView
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public ChannelType Channel { get; set; }
    public bool IsOptOut { get; set; }
}

public sealed class PreferenceViewProjection
{
    public static PreferenceView Create(PreferenceView view, PreferenceCreatedIntegrationEvent @event)
    {
        view.UserId = @event.UserId;
        view.Channel = @event.Channel;
        view.IsOptOut = @event.IsOptOut;

        return view;
    }
    public static PreferenceView Apply(PreferenceView view, PreferenceUpdatedIntegrationEvent @event)
    {
        view.IsOptOut = @event.IsOptOut;

        return view;
    }

    
}