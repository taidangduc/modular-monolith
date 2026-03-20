using BuildingBlocks.EFCore;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ModularMonolith.Preference.ConfigurationOptions;
using ModularMonolith.Preference.Grpc.Services;
using ModularMonolith.Preference.Infrastructure;

namespace ModularMonolith.Preference.Extensions;

public static class ApplicationServicesExtensions
{
    public static WebApplicationBuilder AddPreferenceModules(this WebApplicationBuilder builder, Action<PreferenceModuleOptions> configureOptions)
    {
        var options = new PreferenceModuleOptions();
        configureOptions(options);

        builder.Services.Configure(configureOptions);

        builder.AddCustomDbContext<PreferenceDbContext>(options.ConnectionStrings);

        builder.Services.AddValidatorsFromAssembly(typeof(PreferenceRoot).Assembly);
        builder.Services.AddCustomMediatR();

        builder.Services.AddGrpc();

        return builder;
    }
    public static WebApplication UsePreferenceModules(this WebApplication app)
    {
        app.MapGrpcService<PreferenceService>();

        return app;
    }
}
