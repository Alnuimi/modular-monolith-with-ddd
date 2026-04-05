using System.Text.Json.Serialization;
using Articles.Security;
using Auth.Grpc;
using Blocks.AspNetCore.Grpc;
using Blocks.Core.Extensions;
using Blocks.Messaging;
using FileStorage.MongoGridFS;
using Journals.Grpc;
using Microsoft.AspNetCore.Http.Json;

namespace Submission.API;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureApiOptions(this IServiceCollection services, IConfiguration configuration)
    {
        // use it for configuring the options
        services
            .AddAndValidateOptions<RabbitMqOptions>(configuration)
            .AddAndValidateOptions<JwtOptions>(configuration)
            .Configure<JsonOptions>(opt =>
            {
                opt.SerializerOptions.PropertyNameCaseInsensitive = true;
                opt.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
        
        return services;
    }  
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddMemoryCache()                    // Basic Caching
            .AddEndpointsApiExplorer()           // Minimal API docs (Swagger)
            .AddSwaggerGen();                    // Swagger Setup

        services.AddMongoFileStorage(configuration); // Module FileStorage
        
        // Clients Grpc
        var grpcOptions = configuration.GetSectionByTypeName<GrpcServicesOptions>();
        services.AddCodeFirstGrpcClient<IPersonService>(grpcOptions, "Person");
        services.AddCodeFirstGrpcClient<IJournalService>(grpcOptions, "Journal");
        return services;
    }
}