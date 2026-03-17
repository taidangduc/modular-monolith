using BuildingBlocks.EFCore;
using BuildingBlocks.Mapster;
using BuildingBlocks.Masstransit;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ModularMonolith.Post.Infrastructure;

namespace ModularMonolith.Post.Extensions;

public static class ApplicationServicesExtensions
{
    public static WebApplicationBuilder AddPostModules(this WebApplicationBuilder builder)
    {
        builder.Services.AddCustomDbContext<PostDbContext>();

        builder.Services.AddValidatorsFromAssembly(typeof(PostRoot).Assembly);
        builder.Services.AddCustomMapster();
        builder.Services.AddCustomMediatR();

        builder.Services.AddSingleton<IMasstransitModule, MasstransitExtensions>();

        return builder;
    }

    public static WebApplication UsePostModules(this WebApplication app)
    {
        return app;
    }
}
