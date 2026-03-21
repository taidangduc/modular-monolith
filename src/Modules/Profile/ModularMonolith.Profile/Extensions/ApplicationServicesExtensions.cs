using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ModularMonolith.BuildingBlocks.EFCore;
using ModularMonolith.BuildingBlocks.EventBus;
using ModularMonolith.Profile.ConfigurationOptions;
using ModularMonolith.Profile.Infrastructure;

namespace ModularMonolith.Profile.Extensions;

public static class ApplicationServicesExtensions
{
    public static WebApplicationBuilder AddProfileModule(this WebApplicationBuilder builder, Action<ProfileModuleOptions> configureOptions)
    {
        var options = new ProfileModuleOptions();
        configureOptions(options);

        builder.Services.Configure(configureOptions);

        builder.AddCustomDbContext<ProfileDbContext>(options.ConnectionStrings);

        builder.Services.AddScoped<IEventMapper, ProfileEventMapper>();

        builder.Services.AddValidatorsFromAssembly(typeof(ProfileRoot).Assembly);

        builder.Services.AddCustomMediatR();

        return builder;
    }
    public static WebApplication UseProfileModule(this WebApplication app)
    {
        return app;
    }

}
