using Articles.IntegrationEvents.Contracts.Dtos;
using FileStorage.Contracts;
using Review.Domain.Assets.Enums;
using Review.Domain.Assets.ValueObjects;

namespace Review.Domain.Assets;

public partial class Asset
{
    private Asset() {}
    
    
    public static Asset CreateFromSubmission(AssetDto assetDto, AssetTypeDefinition type, int articleId)
    {
        var asset = new Asset
        {
            ArticleId = articleId,
            // Article = articleId,
            Name = AssetName.FromAssetType(type),
            State = AssetState.Uploaded,
            Type = type.Id,
        };
        return asset;
    }

    public  File CreateFile(FileMetadata fileMetadata, AssetTypeDefinition assetType)
    {
        File = File.Create(fileMetadata, asset: this, assetType);
        
        State = AssetState.Uploaded;

        return File;
    }
}