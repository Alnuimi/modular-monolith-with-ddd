using System.IO.Compression;
using System.Reflection;
using System.Text.Json.Serialization;
using Articles.Security;
using Auth.Grpc;
using Blocks.AspNetCore.Grpc;
using Blocks.AspNetCore.Providers;
using Blocks.Core.Extensions;
using Blocks.Core.Mapster;
using Blocks.Core.Security;
using Blocks.Messaging;
using Blocks.Messaging.MassTransit;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Http.Json;
using ProtoBuf.Grpc.Server;

namespace Journals.API;

public static class DependenciesInjection
{
    public static IServiceCollection ConfigureApiOptions(this IServiceCollection services, IConfiguration configuration)
    {
        // use it for configuring the options
        services
            .AddAndValidateOptions<JwtOptions>(configuration)
            .AddAndValidateOptions<RabbitMqOptions>(configuration)
            .Configure<JsonOptions>(opt =>
            {
                opt.SerializerOptions.PropertyNameCaseInsensitive = true;
                opt.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
        
        return services;
    }  
    
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration config)
    {
        services
            .AddFastEndpoints()
            .AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .AddJwtAuthentication(config)
            .AddMapsterConfigsFromCurrentAssembly()
            .AddAuthorization()
            .AddMassTransitWithRabbitMq(config, Assembly.GetExecutingAssembly());

        services
            .AddScoped<IClaimsProvider, HttpContextProvider>();
        
        // Server Grpc
        services.AddCodeFirstGrpc(options =>
        {
            options.ResponseCompressionLevel = CompressionLevel.Fastest;
            options.EnableDetailedErrors = true;
            options.Interceptors.Add<AssignUserIdInterceptor>();
        });
        
        // Clients Grpc
        var grpcOptions = config.GetSectionByTypeName<GrpcServicesOptions>();
        services.AddCodeFirstGrpcClient<IPersonService>(grpcOptions, "Person");
        
        return services;
    }
}