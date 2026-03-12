namespace ModularMonolith.Notification.IntegrationEvents.Events;

public class PreferenceUpdatedIntegrationEvent
{
    public Guid UserId { get; set; }
    public bool EmailEnabled { get; set; }
    public bool PushEnabled { get; set; }
    public bool SmsEnabled { get; set; }
}
