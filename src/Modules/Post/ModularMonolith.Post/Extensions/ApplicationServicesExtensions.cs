using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ModularMonolith.BuildingBlocks.EFCore;
using ModularMonolith.BuildingBlocks.EventBus;
using ModularMonolith.Post.ConfigurationOptions;
using ModularMonolith.Post.Infrastructure;

namespace ModularMonolith.Post.Extensions;

public static class ApplicationServicesExtensions
{
    public static WebApplicationBuilder AddPostModule(this WebApplicationBuilder builder, Action<PostModuleOptions> configureOptions)
    {
        var options = new PostModuleOptions();
        configureOptions(options);

        builder.Services.Configure(configureOptions);
    
        builder.AddCustomDbContext<PostDbContext>(options.ConnectionStrings);

        builder.Services.AddScoped<IEventMapper, PostEventMapper>();

        builder.Services.AddValidatorsFromAssembly(typeof(PostRoot).Assembly);
        builder.Services.AddCustomMediatR();

        return builder;
    }

    public static WebApplication UsePostModule(this WebApplication app)
    {
        return app;
    }
}
