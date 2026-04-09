namespace FileStorage.Contracts;

public record FileMetadata(string StoragePath, string FileName, string ContentType, long FileSize, string FileId);