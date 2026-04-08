using Articles.Abstractions.Events.Dtos;
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
            File = File.CreateFromSubmission(assetDto.File, type)
        };
        return asset;
    }
}