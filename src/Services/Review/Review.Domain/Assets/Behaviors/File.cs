using Articles.Abstractions.Events.Dtos;
using Review.Domain.Assets.ValueObjects;

namespace Review.Domain.Assets;

public partial class File
{
    private File(){}

    internal static File CreateFromSubmission(FileDto fileDto, AssetTypeDefinition assetType)
    {
        var extension = FileExtension.FromFileName(fileDto.OriginalName, assetType);
        var file = new File()
        {
            Name = FileName.Create(fileDto.Name),
            Extension = extension,
            OriginalName = fileDto.OriginalName,
            Size =  fileDto.Size,
            FileServerId = fileDto.FileServerId
        };
        
        return file;
    }
}