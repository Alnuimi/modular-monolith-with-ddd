using System.Text.Json.Serialization;
using Blocks.Core.Extensions;
using Blocks.Messaging;
using Carter;
using Microsoft.AspNetCore.Http.Json;
using Articles.Security;
using Blocks.Core.Mapster;
using Blocks.Messaging.MassTransit;
using System.Reflection;
using ArticleHub.Persistence;
using Blocks.Core.Security;
using Blocks.AspNetCore.Providers;

namespace ArticleHub.API;

public static class DependencyInjection
{
    public static void ConfigureApiOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddAndValidateOptions<RabbitMqOptions>(configuration)
            .AddAndValidateOptions<HasuraOptions>(configuration)
            .Configure<JsonOptions>(opt =>
            {
                opt.SerializerOptions.PropertyNameCaseInsensitive = true;
                opt.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
    }

    public static IServiceCollection AddApiAndApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        // API + Infra
        services
            .AddCarter()
            .AddHttpContextAccessor()                    // For accessing HTTP context
            .AddEndpointsApiExplorer()                  // Minimal API docs (Swagger)
            .AddSwaggerGen()                             // Swagger setup
            .AddJwtAuthentication(configuration)         // JWT Authentication
            .AddAuthorization();                         // Authorization configuration


        services
            .AddMemoryCache()
            .AddMapsterConfigsFromCurrentAssembly()
            .AddMassTransitWithRabbitMq(configuration, Assembly.GetExecutingAssembly());

        // http
        services
            .AddScoped<IClaimsProvider, HttpContextProvider>()
            .AddScoped<HttpContextProvider>();
        return services;
    }
}
