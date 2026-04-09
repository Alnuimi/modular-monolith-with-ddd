using System;
using FileStorage.Contracts;
using FileStorage.MongoGridFS;
using Review.API.FileStorage;
using Review.Application.Features.FileStorage;

namespace Review.API;

public static class DependencyInjection
{
    public static IServiceCollection AddFileServiceFactory(this IServiceCollection services)
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


