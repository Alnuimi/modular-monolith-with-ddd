using System;
using Articles.Security;
using Auth.Grpc;
using Blocks.AspNetCore.Grpc;
using Blocks.Core.Extensions;
using Carter;
using EmailService.Smtp;
using FileStorage.Contracts;
using FileStorage.MongoGridFS;
using Review.API.FileStorage;
using Review.Application.Features.FileStorage;

namespace Review.API;

public static class DependencyInjection
{

    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddMemoryCache() // Basic caching
            .AddCarter()      // Register Minimal API endpoints with Carter
            .AddHttpContextAccessor() // For accessing HttpContext in services
            .AddEndpointsApiExplorer() // For Swagger documentation
            .AddSwaggerGen()            // Swagger setup
            .AddJwtAuthentication(configuration) // JWT Authentication
            .AddAuthorization(); // Authorization policies
        

        // external services 
        services.AddMongoFileStorageAsSingleton(configuration);
        services.AddMongoFileStorageAsScoped<SubmissionFileStorageOptions>(configuration);
        services.AddFileServiceFactory();

        services.AddSmtpEmailService(configuration);

        // grpc Services
        var grpcOptions = configuration.GetSectionByTypeName<GrpcServicesOptions>();
        services.AddCodeFirstGrpcClient<IPersonService>(grpcOptions, "Person");
        return services;
    }

    // todo - move to new file 
    private static IServiceCollection AddFileServiceFactory(this IServiceCollection services)
    {
        services.AddScoped<FileServiceFactory>(serviceProvider => fileStorageType =>
        {
            return fileStorageType switch
            {
                FileStorageType.Submission => serviceProvider.GetRequiredService<IFileService<SubmissionFileStorageOptions>>(),
                FileStorageType.Review => serviceProvider.GetRequiredService<IFileService<MongoGridFsFileStorageOptions>>(),
                
                _ => throw new ArgumentException($"Unsupported file storage type: {fileStorageType}")
            };
        });

        return services;
    }
}


