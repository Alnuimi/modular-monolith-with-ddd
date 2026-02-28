using FileStorage.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace FileStorage.MongoGridFS;

public sealed class FileService(GridFSBucket bucket, IOptions<MongoGridFsFileStorageOptions> options)
    : IFileService
{
    private readonly GridFSBucket _bucket = bucket;
    private readonly MongoGridFsFileStorageOptions  _options = options.Value;
    
    private const string FilePathMetadataKey = "filePath";
    private const string ContentTypeMetadataKey = "contentType";
    private const string DefaultContentType = "application/octet-stream";

    public async Task<UploadResponse> UploadFileAsync(
        string filePath,
        IFormFile file,
        bool overwrite = false,
        Dictionary<string, string>? tags = null)
    {
        if(file.Length > _options.FileSizeLimitInBytes)
            throw new InvalidOperationException($"File exceeds maximum allowed limit of {_options.FileSizeLimitInMb} MB.");

        var metadata = new BsonDocument(tags ?? new Dictionary<string, string>())
        {
            {FilePathMetadataKey, filePath},
            {ContentTypeMetadataKey, file.ContentType},
        };

        var uploadOptions = new GridFSUploadOptions
        {
            Metadata = metadata,
            ChunkSizeBytes = _options.ChunkSizeBytes
        };

        ObjectId fileId;
        using (var stream = file.OpenReadStream())
        {
            fileId = await _bucket.UploadFromStreamAsync(file.FileName, stream, uploadOptions);
        }

        return new UploadResponse(
            FilePath: filePath,
            FileName: file.FileName,
            FileSize: file.Length,
            FileId: fileId.ToString());
    }

    public async Task<(Stream FileStream, string ContentType)> DownloadFileAsync(string fileId)
    {
        if(!ObjectId.TryParse(fileId, out var objectId))
            throw new FileNotFoundException($"invalid file Id: {fileId}");
       
        var fileInfo = await _bucket.Find(Builders<GridFSFileInfo>.Filter.Eq("_id", objectId)).FirstOrDefaultAsync();
        if(fileInfo is null)
            throw new FileNotFoundException($"No file found with Id: {fileId}");

        var stream = await _bucket.OpenDownloadStreamAsync(fileId);
        var contentType = fileInfo.Metadata?.GetValue(ContentTypeMetadataKey, DefaultContentType)?.ToString() ??
                          DefaultContentType;
        return (stream, contentType);
    }

    public async Task<bool> TryDeleteFileAsync(string fileId)
    {
        if (!ObjectId.TryParse(fileId, out var objectId))
            return false;

        try
        {
            await _bucket.DeleteAsync(fileId);
            return true;
        }
        catch (GridFSFileNotFoundException)
        {
            return false;
        }

    }
}