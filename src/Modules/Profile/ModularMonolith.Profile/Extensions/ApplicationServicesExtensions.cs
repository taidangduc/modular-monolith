using BuildingBlocks.EFCore;
using BuildingBlocks.Mapster;
using BuildingBlocks.Masstransit;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ModularMonolith.Profile.Infrastructure;

namespace ModularMonolith.Profile.Extensions;

public static class ApplicationServicesExtensions
{
    public static WebApplicationBuilder AddProfileModules(this WebApplicationBuilder builder)
    {
        builder.Services.AddCustomDbContext<ProfileDbContext>();
       
        builder.Services.AddSingleton<IMasstransitModule, MasstransitExtensions>();

        builder.Services.AddValidatorsFromAssembly(typeof(ProfileRoot).Assembly);

        builder.Services.AddCustomMapster();
        builder.Services.AddCustomMediatR();

        return builder;
    }
    public static WebApplication UseProfileModules(this WebApplication app)
    {
        return app;
    }

}
