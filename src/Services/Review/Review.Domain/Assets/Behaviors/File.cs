using Articles.Abstractions.Events.Dtos;
using FileStorage.Contracts;
using Review.Domain.Assets.ValueObjects;

namespace Review.Domain.Assets;

public partial class File
{
    private File(){}

    internal static File Create(FileMetadata fileMetadata, Asset asset, AssetTypeDefinition assetType)
    {
        var fileName = System.IO.Path.GetFileName(fileMetadata.StoragePath);
        var extension = FileExtension.FromFileName(fileName, assetType);

        var file = new File()
        {
            Name = FileName.FromAsset(asset, extension),
            Extension = extension,
            OriginalName = fileName,
            Size = fileMetadata.FileSize,
            FileServerId = fileMetadata.FileId
        };
        
        return file;
    }
}