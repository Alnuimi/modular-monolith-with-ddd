using System;
using System.Text.Json.Serialization;
using Articles.Security;
using Auth.Grpc;
using Blocks.AspNetCore.Grpc;
using Blocks.Core.Extensions;
using Blocks.Messaging;
using Carter;
using EmailService.Contracts;
using EmailService.Smtp;
using FileStorage.Contracts;
using FileStorage.MongoGridFS;
using Microsoft.AspNetCore.Http.Json;
using Review.API.FileStorage;
using Review.Application.Features.FileStorage;

namespace Review.API;

public static class DependencyInjection
{

    public static void ConfigureApiOptions(this IServiceCollection services, IConfiguration configuration)
    {
        // use it for configuring the options
        services
            .AddAndValidateOptions<RabbitMqOptions>(configuration)
            .AddAndValidateOptions<SubmissionFileStorageOptions>(configuration)
            .AddAndValidateOptions<EmailOptions>(configuration)
            .AddAndValidateOptions<GrpcServicesOptions>(configuration)
            .AddAndValidateOptions<JwtOptions>(configuration)
            .Configure<JsonOptions>(opt =>
            {
                opt.SerializerOptions.PropertyNameCaseInsensitive = true;
                opt.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
    } 
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


