using BuildingBlocks.EFCore;
using BuildingBlocks.Mapster;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ModularMonolith.Preference.Grpc.Services;
using ModularMonolith.Preference.Infrastructure;

namespace ModularMonolith.Preference.Extensions;

public static class ApplicationServicesExtensions
{
    public static WebApplicationBuilder AddPreferenceModules(this WebApplicationBuilder builder)
    {
        builder.Services.AddCustomDbContext<PreferenceDbContext>();

        builder.Services.AddValidatorsFromAssembly(typeof(PreferenceRoot).Assembly);
        builder.Services.AddCustomMapster();
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
