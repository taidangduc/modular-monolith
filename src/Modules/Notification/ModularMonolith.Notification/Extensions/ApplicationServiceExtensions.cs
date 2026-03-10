
using BuildingBlocks.EFCore;
using BuildingBlocks.Mapster;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ModularMonolith.Notification.Infrastructure;

namespace ModularMonolith.Notification.Extensions;

public static class ApplicationServiceExtensions
{
    public static WebApplicationBuilder AddNotificationModules(this WebApplicationBuilder builder)
    {
        builder.Services.AddCustomDbContext<NotificationDbContext>();

        builder.Services.AddScoped<NotificationEventMapper>();
       
        builder.Services.AddCustomMapster(typeof(NotificationRoot).Assembly);
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
