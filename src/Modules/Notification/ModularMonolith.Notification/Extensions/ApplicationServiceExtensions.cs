using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ModularMonolith.BuildingBlocks.EFCore;
using ModularMonolith.BuildingBlocks.EventBus;
using ModularMonolith.Notification.ConfigurationOptions;
using ModularMonolith.Notification.Infrastructure;

namespace ModularMonolith.Notification.Extensions;

public static class ApplicationServiceExtensions
{
    public static WebApplicationBuilder AddNotificationModules(this WebApplicationBuilder builder, Action<NotificationModuleOptions> configureOptions)
    {
        var options = new NotificationModuleOptions();
        configureOptions(options);

        builder.Services.Configure(configureOptions);

        builder.AddCustomDbContext<NotificationDbContext>(options.ConnectionStrings);
        builder.AddCustomDbContext<NotificationReadDbContext>(options.ConnectionStrings);

        builder.Services.AddScoped<IEventMapper, NotificationEventMapper>();

        builder.Services.AddFluentValidation(x => x.RegisterValidatorsFromAssembly(typeof(NotificationRoot).Assembly));

        builder.Services.AddCustomMediatR();
        builder.Services.AddCustomGrpcClient();

        return builder;
    }
    public static WebApplication UseNotificationModules(this WebApplication app)
    {
        return app;
    }
}
