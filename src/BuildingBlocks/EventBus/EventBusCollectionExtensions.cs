using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using ModularMonolith.BuildingBlocks.Exceptions;
using System.Reflection;

namespace ModularMonolith.BuildingBlocks.EventBus;

//ref: https://masstransit.io/documentation/concepts/exceptions
//ref: https://masstransit.io/documentation/configuration/middleware/scoped
//ref: https://stackoverflow.com/questions/17326185/what-are-the-different-kinds-of-cases
//ref: https://markgossa.com/2022/06/masstransit-exponential-back-off.html
//ref: https://www.jbw.codes/blog/mass-transit-consumer

public static class EventBusCollectionExtensions
{
    public static IServiceCollection AddEventBus(
      this IServiceCollection services,
      EventBusOptions options,
      params Assembly[] assemblies)
    {   
        services.AddMassTransit(configure =>
        {
            configure.SetKebabCaseEndpointNameFormatter();
            configure.AddConsumers(assemblies);

            if(options.UsedRabbitMQ())
            {
                services.AddRabbitMQTransport(configure, options.RabbitMQ);
            }
            else
            {
                services.AddInMemoryTransport(configure);
            }

        });

        return services;
    }

    public static IServiceCollection AddEventBusDispatcher(this IServiceCollection services)
    {
        services.AddScoped<IEventDispatcher, EventDispatcher>();
        return services;
    }

    public static IServiceCollection AddRabbitMQTransport(this IServiceCollection services, IBusRegistrationConfigurator configure, RabbitMQOptions options)
    {
        configure.UsingRabbitMq((context, config) =>
        {
            config.Host(
                options.HostName, 
                options.Port ?? 5176,
                "/",
                opt =>
                {
                    opt.Username(options.UserName);
                    opt.Password(options.Password);
                });

            config.ConfigureEndpoints(context);
            config.UseMessageRetry(AddRetryConfiguration);
        });


        return services;
    }

    public static IServiceCollection AddInMemoryTransport(this IServiceCollection services, IBusRegistrationConfigurator configure)
    {
        configure.UsingInMemory((context, config) =>
        {
            config.ConfigureEndpoints(context);
            config.UseMessageRetry(AddRetryConfiguration);
        });

        return services;
    }

    private static void AddRetryConfiguration(IRetryConfigurator configurator)
    {
        configurator.Exponential(
                3,
                TimeSpan.FromMilliseconds(200),
                TimeSpan.FromMinutes(30),
                TimeSpan.FromMilliseconds(200))
            .Ignore<ValidationException>();
    }
}
