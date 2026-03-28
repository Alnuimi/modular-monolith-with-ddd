using System.Text.Json.Serialization;
using Articles.Security;
using Auth.Grpc;
using Blocks.AspNetCore.Grpc;
using Blocks.Core.Extensions;
using Blocks.Core.Mapster;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Http.Json;

namespace Journals.API;

public static class DependenciesInjection
{
    public static IServiceCollection ConfigureApiOptions(this IServiceCollection services, IConfiguration configuration)
    {
        // use it for configuring the options
        services
            .AddAndValidateOptions<JwtOptions>(configuration)
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
            // .AddJwtAuthentication(config)
            .AddMapster()
            .AddAuthorization();

        var grpcOptions = config.GetSectionByTypeName<GrpcServicesOptions>();
        services.AddCodeFirstGrpcClient<IPersonService>(grpcOptions, "Person");
        
        return services;
    }
}