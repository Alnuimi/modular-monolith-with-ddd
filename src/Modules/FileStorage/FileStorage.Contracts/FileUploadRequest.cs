namespace FileStorage.Contracts;

public record FileUploadRequest(string StoragePath, string FileName, string ContentType, long FileSize = default);
