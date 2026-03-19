using ModularMonolith.Notification.IntegrationEvents.Events;

namespace ModularMonolith.Notification.Infrastructure.Projections;

public class PreferenceView
{
    public Guid UserId { get; set; }
    public ChannelType Channel { get; set; }
    public bool IsOptOut { get; set; }
}

public enum ChannelType
{
    Email,
    Web,
    Sms
}

public class PreferenceViewProjection : IProjection<PreferenceUpdatedIntegrationEvent>
{
    private readonly NotificationReadDbContext _readDbContext;

    public PreferenceViewProjection(NotificationReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task ProjectAsync(PreferenceUpdatedIntegrationEvent @event)
    {
        if (!Enum.TryParse<ChannelType>(@event.Channel, true, out var channel))
        {
            // Unknown channel, ignore or handle as needed
            return;
        }

        var preference = _readDbContext.preferenceView.FirstOrDefault(p => p.UserId == @event.UserId && p.Channel == channel);
        if (preference is null)
        {
            preference = new PreferenceView
            {
                UserId = @event.UserId,
                Channel = channel,
                IsOptOut = @event.IsOptOut
            };
            _readDbContext.preferenceView.Add(preference);
        }
        else
        {
            preference.IsOptOut = @event.IsOptOut;
        }

        await _readDbContext.SaveChangesAsync();
    }
}
