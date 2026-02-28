using FileStorage.Contracts;
using Submission.Domain.ValueObjects;

namespace Submission.Domain.Entities;

public partial class Asset
{
    private Asset(){}
   
    internal static Asset Create(Article article, AssetTypeDefinition type)
    {
        return new Asset()
        {
            ArticleId = article.Id,
            Article = article,
            Name = AssetName.FromAssetTypr(type),
            Type = type.Name
        };
    }

    public string GenerateStorageFilePath(string fileName)
        => $"Articles/{ArticleId}/{Name}/{fileName}";

    public File CreateFile(UploadResponse uploadResponse, AssetTypeDefinition assetType)
    {
        File = File.Create(
            uploadResponse: uploadResponse, 
            asset: this,
            assetType: assetType);
        return File;
    }
}