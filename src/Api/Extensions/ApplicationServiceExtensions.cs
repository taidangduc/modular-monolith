using BuildingBlocks.Web;
using FluentValidation.AspNetCore;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ModularMonolith.Api.ConfigurationOptions;
using ModularMonolith.BuildingBlocks.EventBus;
using ModularMonolith.Identity;
using ModularMonolith.Notification;
using ModularMonolith.Preference;
using ModularMonolith.Profile;
using System.Reflection;

namespace ModularMonolith.Api.Extensions;

//ref: https://learn.microsoft.com/en-us/aspnet/core/signalr/configuration?view=aspnetcore-9.0&tabs=dotnet
public static class ApplicationServiceExtensions
{
    public static WebApplicationBuilder AddApplicationServices(this WebApplicationBuilder builder, AppSettings appSettings)
    {
        Assembly[] assemblies = new Assembly[]
        {
            typeof(IdentityRoot).Assembly,
            typeof(NotificationRoot).Assembly,
            typeof(ProfileRoot).Assembly,
            typeof(PreferenceRoot).Assembly,
            typeof(NotificationRoot).Assembly
        };

        builder.Services.AddSignalR();

        builder.Services.AddControllers();

        builder.Services.AddFluentValidation();

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.Authority = appSettings.Authentication.IdentityServer.Authority;
            options.Audience = appSettings.Authentication.IdentityServer.Audience;
            options.RequireHttpsMetadata = true;

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuers = [appSettings.Authentication.IdentityServer.Authority, "https://localhost:7265/"],
                ValidateAudience = true,
                ValidAudiences = [appSettings.Authentication.IdentityServer.Audience],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromSeconds(2),
                ValidateIssuerSigningKey = true,
                NameClaimType = "name", // Map "name" claim to User.Identity.Name
                RoleClaimType = "role", // Map "role" claim to User.IsInRole()
            };
            options.MapInboundClaims = false;
        });

        builder.Services.AddSwaggerGen(
            options =>
            {

                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Modular Monolith API",
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

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();

        builder.Services.AddEventBusDispatcher();
        builder.Services.AddEventBus(appSettings.EventBus, assemblies);

        builder.Services.AddMemoryCache();

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
