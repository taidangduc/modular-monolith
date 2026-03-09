
using BuildingBlocks.EFCore;
using BuildingBlocks.Mapster;
using FluentValidation;
using Identity.Infrastructure;
using Identity.Infrastructure.Seeds;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Extensions;

public static class ApplicationServicesExtensions
{
    public static WebApplicationBuilder AddIdentityModules(this WebApplicationBuilder builder)
    {

        builder.Services.AddScoped<IdentityEventMapper>();
        builder.Services.AddScoped<IDataSeeder, UserSeeder>();

        builder.Services.AddCustomMapster(typeof(IdentityRoot).Assembly);
        builder.Services.AddValidatorsFromAssembly(typeof(IdentityRoot).Assembly);
        builder.Services.AddMediatRCustom();
        
        builder.Services.AddCustomIdentityContext();
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
        app.UseMigration<IdentityDbContext>();

        return app;
    }
}
