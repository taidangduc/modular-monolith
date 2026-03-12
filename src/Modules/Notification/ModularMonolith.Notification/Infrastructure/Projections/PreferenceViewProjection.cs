using ModularMonolith.Notification.IntegrationEvents.Events;

namespace ModularMonolith.Notification.Infrastructure.Projections;

public class PreferenceView
{
    public Guid UserId { get; set; }
    public bool EmailEnabled { get; set; }
    public bool PushEnabled { get; set; }
    public bool SmsEnabled { get; set; }
}
public class PreferenceViewProjection : IProjection<PreferenceUpdatedIntegrationEvent>
{
    private readonly NotificationReadDbContext _readDbContext;

    public PreferenceViewProjection(NotificationReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task ProjectAsync (PreferenceUpdatedIntegrationEvent @event)
    {
        var preference = _readDbContext.preferenceView.FirstOrDefault(p => p.UserId == @event.UserId);

        if (preference is not null)
        {
            preference.EmailEnabled = @event.EmailEnabled;
            preference.PushEnabled = @event.PushEnabled;
            preference.SmsEnabled = @event.SmsEnabled;
        }
        else
        {
            var newPreference = new PreferenceView
            {
                UserId = @event.UserId,
                EmailEnabled = @event.EmailEnabled,
                PushEnabled = @event.PushEnabled,
                SmsEnabled = @event.SmsEnabled
            };
            _readDbContext.preferenceView.Add(newPreference);
        }
        
        await _readDbContext.SaveChangesAsync();
    }

}