namespace ModularMonolith.Notification.IntegrationEvents.Events;

public class ProfileUpdatedIntegrationEvent
{
    public Guid UserId { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
}
