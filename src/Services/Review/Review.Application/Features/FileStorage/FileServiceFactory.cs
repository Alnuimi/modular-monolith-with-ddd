using System;
using FileStorage.Contracts;

namespace Review.Application.Features.FileStorage;

public enum FileStorageType
{
    Review,
    Submission
}
public delegate IFileService FileServiceFactory(FileStorageType fileStorageType);