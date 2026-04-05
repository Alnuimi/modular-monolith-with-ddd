using System.Reflection;
using Blocks.Core.Extensions;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blocks.Messaging.MassTransit;

public static class DependencyInjection
{
    public static IServiceCollection AddMassTransitWithRabbitMq(
        this IServiceCollection services, IConfiguration configuration, Assembly assembly)
    {
        var rabbitMqOptions = configuration.GetSectionByTypeName<RabbitMqOptions>();

        var serviceName = assembly.GetServiceName();
        
        services.AddMassTransit(config =>
        {
            if (assembly is not null)
            {
                config.AddConsumers(assembly);
            }
            
            config.SetEndpointNameFormatter(new SnakeCaseWithServiceSuffixNameFormatter(serviceName));
            
            config.UsingRabbitMq((context, rabbitConfig) =>
            {
                rabbitConfig.Host(new Uri(rabbitMqOptions.Host), rabbitMqOptions.VirtualHost, h =>
                {
                    h.Username(rabbitMqOptions.Username);
                    h.Password(rabbitMqOptions.Password);
                });
                rabbitConfig.ConfigureEndpoints(context);
            });
        });
        return services;
    }
}