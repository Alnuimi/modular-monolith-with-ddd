using Blocks.Core.Extensions;
using FileStorage.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace FileStorage.MongoGridFS;

public static class FileStorageRegistration
{
    public static IServiceCollection AddMongoFileStorageAsSingleton(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAndValidateOptions<MongoGridFsFileStorageOptions>(configuration);
        var options = configuration.GetSectionByTypeName<MongoGridFsFileStorageOptions>();

        services.AddSingleton<IMongoClient>(sp => new MongoClient(configuration.GetConnectionStringOrThrow(options.ConnectionStringName)));

        services.AddSingleton(sp =>
        {
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase(options.DatabaseName);
        });

        services.AddSingleton(sp =>
        {
            var db = sp.GetRequiredService<IMongoDatabase>();

            return new GridFSBucket(db, new GridFSBucketOptions
            {
                BucketName = options.BucketName,
                ChunkSizeBytes = options.ChunkSizeBytes,
                WriteConcern = WriteConcern.WMajority,
                ReadPreference = ReadPreference.Primary
            });
        });

        services.AddSingleton<IFileService, FileService>();

        return services;
    }

    public static IServiceCollection AddMongoFileStorageAsScoped<TOptions>(this IServiceCollection services,
        IConfiguration configuration)
        where TOptions : MongoGridFsFileStorageOptions
    {
        services.AddAndValidateOptions<TOptions>(configuration);
        
        // TOptions is mandatory here so the DI will be able to register multiple IFileService
        services.AddScoped<IFileService<TOptions>>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<TOptions>>();
            var optionsValue = options.Value;
            var client = new MongoClient(configuration.GetConnectionStringOrThrow(optionsValue.ConnectionStringName));
            var db = client.GetDatabase(optionsValue.DatabaseName);
            var bucket = new GridFSBucket(db, new GridFSBucketOptions
            {
                BucketName = optionsValue.BucketName,
                ChunkSizeBytes = optionsValue.ChunkSizeBytes,
                WriteConcern = WriteConcern.WMajority,
                ReadPreference = ReadPreference.Primary
            });

            return new FileService<TOptions>(bucket, options);
        });

        return services;
    }
}