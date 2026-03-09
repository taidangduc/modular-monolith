using BuildingBlocks.Contracts;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using User.Domain.Entities;
using User.Infrastructure;

namespace User.IntegrationEvents.EventHandlers;

public class UserCreatedEventHandler : IConsumer<UserCreated>
{
    private readonly UserDbContext _userDbContext;
    private readonly ILogger<UserCreatedEventHandler> _logger;

    public UserCreatedEventHandler(UserDbContext userDbContext, ILogger<UserCreatedEventHandler> logger)
    {
        _userDbContext = userDbContext;
        _logger = logger;
    }
    public async Task Consume(ConsumeContext<UserCreated> context)
    {
        _logger.LogInformation($"Consumer for {nameof(UserCreated)} started");

        var data = context.Message;

        if (data.Equals(default))
        {
            return;
        }

        var profileEntity = await _userDbContext.Profiles.FirstOrDefaultAsync(x => x.UserId == data.UserId);

        if (profileEntity != null)
        {
            return;
        }

        var profile = Profile.Create(data.UserId, data.UserName, data.Name, data.Email);
        await _userDbContext.Profiles.AddAsync(profile);

        var preferences = Preference.SetDefaultValues(data.UserId);
        await _userDbContext.Preferences.AddRangeAsync(preferences);

        await _userDbContext.SaveChangesAsync();
    }
}
