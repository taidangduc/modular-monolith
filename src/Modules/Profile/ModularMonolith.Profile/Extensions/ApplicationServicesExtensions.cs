using BuildingBlocks.EFCore;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using ModularMonolith.Profile.Infrastructure;

namespace ModularMonolith.Profile.Extensions;

public static class ApplicationServicesExtensions
{
    public static WebApplicationBuilder AddProfileModules(this WebApplicationBuilder builder)
    {
        builder.Services.AddCustomDbContext<ProfileDbContext>();
       
        builder.Services.AddValidatorsFromAssembly(typeof(ProfileRoot).Assembly);

        builder.Services.AddCustomMediatR();

        return builder;
    }
    public static WebApplication UseProfileModules(this WebApplication app)
    {
        return app;
    }

}
