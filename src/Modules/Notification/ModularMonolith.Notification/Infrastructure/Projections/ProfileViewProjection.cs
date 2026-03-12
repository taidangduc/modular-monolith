using Microsoft.EntityFrameworkCore;
using ModularMonolith.Notification.IntegrationEvents.Events;

namespace ModularMonolith.Notification.Infrastructure.Projections;

public class ProfileView
{
    public Guid UserId { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
}
public class ProfileViewProjection : IProjection<ProfileUpdatedIntegrationEvent>
{
    private readonly NotificationReadDbContext _readDbContext;

    public ProfileViewProjection(NotificationReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task ProjectAsync (ProfileUpdatedIntegrationEvent @event)
    {
        var profile = await _readDbContext.profileView.FirstOrDefaultAsync(p => p.UserId == @event.UserId);

        if (profile is not null)
        {
            profile.FullName = @event.FullName;
            profile.Email = @event.Email;

        }
        else
        {
            var newProfile = new ProfileView
            {
                UserId = @event.UserId,
                FullName = @event.FullName,
                Email = @event.Email
            };
            _readDbContext.profileView.Add(newProfile);
        }

        await _readDbContext.SaveChangesAsync();
    }
}