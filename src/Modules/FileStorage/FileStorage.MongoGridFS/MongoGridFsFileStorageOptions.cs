using System.ComponentModel.DataAnnotations;

namespace FileStorage.MongoGridFS;

public sealed class MongoGridFsFileStorageOptions
{
    [Required]
    public string ConnectionStringName { get; init; } = default!;
    
    [Required]
    public string DatabaseName { get; init; } = default!;
    public string BucketName { get; init; } = "files";
    public int ChunkSizeBytes { get; init; } = 1048576; // 1MB GridFS splits files into chunks of this size when storing
    public long FileSizeLimitInMb { get; init; } = 50;
    public long FileSizeLimitInBytes => FileSizeLimitInMb * 1024 * 1024;
}