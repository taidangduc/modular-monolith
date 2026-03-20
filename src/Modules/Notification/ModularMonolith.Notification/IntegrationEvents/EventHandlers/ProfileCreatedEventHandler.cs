using MassTransit;
using ModularMonolith.Notification.Infrastructure;
using ModularMonolith.Notification.Infrastructure.Projections;
using ModularMonolith.Notification.IntegrationEvents.Events;

namespace ModularMonolith.Notification.IntegrationEvents.EventHandlers;

public class ProfileCreatedEventHandler : IConsumer<ProfileCreatedIntegrationEvent>
{
	private readonly NotificationReadDbContext _readDbContext;

	public ProfileCreatedEventHandler(NotificationReadDbContext readDbContext)
	{
		_readDbContext = readDbContext;
	}

	public async Task Consume(ConsumeContext<ProfileCreatedIntegrationEvent> context)
	{
		if (context.Message is null)
		{
			return;
		}

		var profile = _readDbContext.profileView
			.FirstOrDefault(p => p.UserId == context.Message.UserId);

		if (profile is null)
		{
			var newProfile = ProfileViewProjection.Create(new ProfileView(), context.Message);
			_readDbContext.profileView.Add(newProfile);
			await _readDbContext.SaveChangesAsync();
		}
	}
}
