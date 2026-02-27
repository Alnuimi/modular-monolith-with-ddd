using Articles.Abstractions.Enums;
using Blocks.Domain.ValueObjects;
using Submission.Domain.Entities;

namespace Submission.Domain.ValueObjects;

public class AssetName : StringValueObject
{
    public AssetName(string value) => Value = value;
    
    public static AssetName FromAssetTypr(AssetTypeDefinition assetType) 
        => new AssetName(assetType.Name.ToString());
}