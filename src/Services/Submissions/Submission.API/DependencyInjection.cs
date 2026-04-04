using Auth.Grpc;
using Blocks.AspNetCore.Grpc;
using Blocks.Core.Extensions;
using FileStorage.MongoGridFS;
using Journals.Grpc;

namespace Submission.API;

public static class DependencyInjection
{
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