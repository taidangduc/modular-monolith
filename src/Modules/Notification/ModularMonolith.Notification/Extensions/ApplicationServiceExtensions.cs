
using BuildingBlocks.EFCore;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ModularMonolith.Notification.Infrastructure;
using ModularMonolith.Notification.Infrastructure.Projections;
using ModularMonolith.Notification.IntegrationEvents.Events;

namespace ModularMonolith.Notification.Extensions;

public static class ApplicationServiceExtensions
{
    public static WebApplicationBuilder AddNotificationModules(this WebApplicationBuilder builder)
    {
        builder.Services.AddCustomDbContext<NotificationDbContext>();
        builder.Services.AddCustomDbContext<NotificationReadDbContext>();

        builder.Services.AddScoped<NotificationEventMapper>();
       
        builder.Services.AddFluentValidation(x => x.RegisterValidatorsFromAssembly(typeof(NotificationRoot).Assembly));

        builder.Services.AddCustomMediatR();
        builder.Services.AddCustomGrpcClient();

        builder.Services.AddScoped<ProjectionDispatcher>();
        builder.Services.AddScoped<IProjection<PreferenceUpdatedIntegrationEvent>, PreferenceViewProjection>();
        builder.Services.AddScoped<IProjection<ProfileUpdatedIntegrationEvent>, ProfileViewProjection>();

        return builder;
    }
    public static WebApplication UseNotificationModules(this WebApplication app)
    {
        return app;
    }
}
