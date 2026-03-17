using BuildingBlocks.Jwt;
using BuildingBlocks.Web;
using FluentValidation.AspNetCore;
using MassTransit;
using Microsoft.OpenApi.Models;
using ModularMonolith.BuildingBlocks.EventBus;
using ModularMonolith.Identity;
using ModularMonolith.Notification;
using System.Reflection;

namespace Api.Extensions;

//ref: https://learn.microsoft.com/en-us/aspnet/core/signalr/configuration?view=aspnetcore-9.0&tabs=dotnet
public static class SharedInfrastructureExtensions
{
    public static WebApplicationBuilder AddApplicationSevices(this WebApplicationBuilder builder)
    {
        Assembly[] assemblies = new Assembly[]
        {
            typeof(IdentityRoot).Assembly,
            typeof(NotificationRoot).Assembly
        };

        builder.Services.AddSignalR();
        //builder.Services.AddJwt();
        builder.Services.AddJwtAuth();

        //persistMessage
        //builder.Services.AddPersistMessageProcessor();

        builder.Services.AddControllers();
       
        //validation
        builder.Services.AddFluentValidation();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();
        builder.Services.AddSwaggerGen(
            options => {

                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "MyApi",
                    Version = "v1",
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authentization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer",
                    Description = "Enter 'Bearer' [space] and your token in the text input below.\n\nExample: 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...'"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            In = ParameterLocation.Header,
                            Name = "X-API-KEY",
                            Type = SecuritySchemeType.ApiKey,
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });
        //builder.Services.AddScoped<IEventDispatcher, EventDispatcher>();
        builder.Services.AddEventBusDispatcher();

        //builder.Services.AddCustomMasstransit(
        //    TransportType.InMemory,
        //    assemblies);

        

        //AppDomain.CurrentDomain.GetAssemblies()
        builder.Services.AddMemoryCache();

        //builder.Services.AddScoped<IEventMapper>(sp =>
        //{
        //    var mappers = new IEventMapper[]
        //    {
        //        sp.GetRequiredService<IdentityEventMapper>(),
        //        sp.GetRequiredService<NotificationEventMapper>()
        //    };
        //    return new CompositEventMapper(mappers);         
        //});

        return builder;
    }

    public static WebApplication UseApplicationServices(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCorrelationId();

        app.MapControllers();
        return app;
    }
}
