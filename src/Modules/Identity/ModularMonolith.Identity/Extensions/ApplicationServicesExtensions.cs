using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModularMonolith.BuildingBlocks.EventBus;
using ModularMonolith.Identity.ConfigurationOptions;
using ModularMonolith.Identity.Infrastructure;
using ModularMonolith.Identity.Infrastructure.HostServices;
using ModularMonolith.Identity.Infrastructure.Seeds;
using static ModularMonolith.BuildingBlocks.EFCore.MigrateDbContextExtentions;

namespace ModularMonolith.Identity.Extensions;

public static class ApplicationServicesExtensions
{
    public static WebApplicationBuilder AddIdentityModules(this WebApplicationBuilder builder, Action<IdentityModuleOptions> configureOptions)
    {
        var settings = new IdentityModuleOptions();
        configureOptions(settings);

        builder.Services.Configure(configureOptions);

        builder.Services.AddDbContext<IdentityDbContext>((sp, options) =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString(settings.ConnectionStrings));
            options.UseOpenIddict();
        });

        builder.AddCustomIdentityServer();

        builder.Services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
        });

        builder.Services.AddScoped<IEventMapper, IdentityEventMapper>();

        // migration
        builder.Services.AddScoped<IDataSeeder<IdentityDbContext>, UserSeeder>();

        builder.Services.AddValidatorsFromAssembly(typeof(IdentityRoot).Assembly);
        builder.Services.AddMediatRCustom();

        // hostservices
        builder.Services.AddHostedService<SeedDataHostServices>();

        return builder;
    }

    public static async Task<WebApplication> UseIdentityModules(this WebApplication app)
    {
        app.UseForwardedHeaders();
        await app.MigrateAndSeedDbContextAsync<IdentityDbContext>();

        return app;
    }
}
