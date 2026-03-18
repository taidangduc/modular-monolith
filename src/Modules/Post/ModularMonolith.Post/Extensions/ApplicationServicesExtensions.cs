using BuildingBlocks.EFCore;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using ModularMonolith.Post.Infrastructure;

namespace ModularMonolith.Post.Extensions;

public static class ApplicationServicesExtensions
{
    public static WebApplicationBuilder AddPostModules(this WebApplicationBuilder builder)
    {
        builder.Services.AddCustomDbContext<PostDbContext>();

        builder.Services.AddValidatorsFromAssembly(typeof(PostRoot).Assembly);
        builder.Services.AddCustomMediatR();

        return builder;
    }

    public static WebApplication UsePostModules(this WebApplication app)
    {
        return app;
    }
}
