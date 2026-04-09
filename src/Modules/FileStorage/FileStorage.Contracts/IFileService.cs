using Microsoft.AspNetCore.Http;

namespace FileStorage.Contracts;


public interface IFileService<TFileStorageOptions> : IFileService
where TFileStorageOptions : IFileStorageOptions ;

public interface IFileService
{
    Task<FileMetadata> UploadFileAsync(string storagePath, IFormFile file, bool overwrite = false, Dictionary<string, string>? tags = null, CancellationToken ct = default);

    Task<FileMetadata> UploadFileAsync(FileUploadRequest request, Stream stream, bool overwrite = false, Dictionary<string, string>? tags = null, CancellationToken ct = default);
    Task<(Stream FileStream, FileMetadata FileMetadata)> DownloadFileAsync(string fileId, CancellationToken ct = default);
    
    Task<bool> TryDeleteFileAsync(string fileId, CancellationToken ct = default);
}
