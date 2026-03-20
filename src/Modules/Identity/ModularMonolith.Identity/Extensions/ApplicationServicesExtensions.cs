using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModularMonolith.Identity.ConfigurationOptions;
using ModularMonolith.Identity.Infrastructure;

namespace ModularMonolith.Identity.Extensions;

public static class ApplicationServicesExtensions
{
    public static WebApplicationBuilder AddIdentityModules(this WebApplicationBuilder builder, Action<IdentityModuleOptions> configureOptions)
    {
        var settings = new IdentityModuleOptions();
        configureOptions(settings);

        builder.Services.Configure(configureOptions);

        builder.Services.AddScoped<IdentityEventMapper>();
        //builder.Services.AddScoped<IDataSeeder, UserSeeder>();

        builder.Services.AddValidatorsFromAssembly(typeof(IdentityRoot).Assembly);
        builder.Services.AddMediatRCustom();

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

        //builder.Services.AddHostedService<ClientAppSeeder>();

        return builder;
    }

    public static WebApplication UseIdentityModules(this WebApplication app)
    {
        app.UseForwardedHeaders();
        //app.UseMigration<IdentityDbContext>();

        return app;
    }
}
